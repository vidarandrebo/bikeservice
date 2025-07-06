import { User } from "./User.ts";
import { getAuthClient } from "../Api.ts";
import { HttpValidationProblemDetails, HttpValidationProblemDetailsFromJSON, ResponseError } from "../../Gen";

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
        const client = getAuthClient();
        try {
            await client.apiAuthRegisterPost({
                registerRequest: { email: this.email, password: this.password }
            });
            return null;
        } catch (e) {
            if (e instanceof ResponseError) {
                return HttpValidationProblemDetailsFromJSON(await e.response.json());
            } else {
                throw e;
            }
        }
    }

    async loginUserRequest(): Promise<User | null> {
        const client = getAuthClient();

        try {
            const response = await client.apiAuthLoginPost({
                loginRequest: {
                    email: this.email,
                    password: this.password,
                    twoFactorCode: null,
                    twoFactorRecoveryCode: null
                }
            });
            const user = new User();
            user.refreshToken = response.refreshToken;
            user.accessToken = response.accessToken;
            user.email = this.email;
            user.writeToLocalStorage();
            return user;
        } catch {
            console.log("failed to login");
        }
        return null;
    }
}
