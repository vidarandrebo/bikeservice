import { HttpRequest } from "http-methods-ts";
import { LoginRequest, RegisterRequest } from "aspnetcore-ts/Identity/Data";
import { HttpValidationProblemDetails } from "aspnetcore-ts/Http";
import { AccessTokenResponse } from "aspnetcore-ts/Authentication/BearerToken";
import { User } from "./User.ts";

export interface ICredentials {
    email: string;
    password: string;
    repeatPassword: string;
    errors: Array<string>;

    registerUserRequest(): Promise<HttpValidationProblemDetails | null>;

    loginUserRequest(): Promise<User | null>;

    passwordRequirementsCheck(): void;
}

export class Credentials implements ICredentials {
    email: string;
    password: string;
    repeatPassword: string;
    errors: Array<string>;

    passwordRequirementsCheck(): void {
        this.errors = new Array<string>();
        if (this.password !== this.repeatPassword) {
            this.errors.push("Passwords do not match");
        }
        if (this.password.length < 8) {
            this.errors.push("Password should be 8 characters or longer!");
        }
    }

    constructor() {
        this.email = "";
        this.repeatPassword = "";
        this.password = "";
        this.errors = new Array<string>();
    }

    async registerUserRequest(): Promise<HttpValidationProblemDetails | null> {
        const registerRequest = new RegisterRequest(this.email, this.password);
        const httpRequest = new HttpRequest()
            .setRoute("/api/register")
            .setMethod("POST")
            .addHeader("Content-Type", "application/json")
            .setRequestData(registerRequest);
        await httpRequest.send();
        const httpResponse = httpRequest.getResponseData();
        const registerErrors = new HttpValidationProblemDetails();

        if (httpResponse) {
            if (httpResponse?.status == 400) {
                registerErrors.assignFromObject(httpResponse.body as Record<string, never>);
                return registerErrors;
            }
        }
        return null;
    }

    async loginUserRequest(): Promise<User | null> {
        // send login user request to server
        const loginRequest = new LoginRequest(this.email, this.password, null, null);
        const httpRequest = new HttpRequest()
            .setRoute("/api/login")
            .setMethod("POST")
            .addHeader("Content-Type", "application/json")
            .setRequestData(loginRequest);
        await httpRequest.send();
        const httpResponse = httpRequest.getResponseData();
        const loginResponse = new AccessTokenResponse();

        if (httpResponse) {
            if (httpResponse?.status == 201) {
                loginResponse.assignFromObject(httpResponse.body as Record<string, never>);
                const user = new User();
                user.refreshToken = loginResponse.refreshToken;
                user.accessToken = loginResponse.accessToken;
                user.email = this.email;
                user.writeToLocalStorage();
                return user;
            }
        }
        return null;
    }
}
