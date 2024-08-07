import { HttpRequest } from "http-methods-ts";
import { FetchResponse, httpPostWithBody } from "../HttpMethods.ts";
import { AuthRouteResponse } from "./AuthRouteResponse.ts";
import { RegisterRequest } from "aspnetcore-ts/Identity/Data";

export interface ICredentials {
    email: string;
    password: string;
    repeatPassword: string;

    registerUserRequest(): Promise<FetchResponse<AuthRouteResponse>>;

    loginUserRequest(): Promise<FetchResponse<AuthRouteResponse>>;

    passwordRequirementsCheck(): void;
}

export class Credentials implements ICredentials {
    email: string;
    password: string;
    repeatPassword: string;
    error: Array<string>;

    passwordRequirementsCheck(): void {
        this.error = new Array<string>();
        if (this.password !== this.repeatPassword) {
            this.error.push("Passwords do not match");
        }
        if (this.password.length < 8) {
            this.error.push("Password should be 8 characters or longer!");
        }
    }

    constructor() {
        this.email = "";
        this.repeatPassword = "";
        this.password = "";
        this.error = new Array<string>();
    }

    async registerUserRequest(): Promise<FetchResponse<AuthRouteResponse>> {
        //return await httpPostWithBody<ICredentials, AuthRouteResponse>("/api/register", this);
        const payload = new RegisterRequest(this.email, this.password);
        const request = new HttpRequest()
            .setRoute("/api/regsiter")
            .setMethod("POST")
            .addHeader("Content-Type", "application/json")
            .setRequestData(payload);
        await request.send();
        const response = request.getResponseData()
        if (response) {
            if (response.status === )
        }
    }

    async loginUserRequest(): Promise<FetchResponse<AuthRouteResponse>> {
        return await httpPostWithBody<ICredentials, AuthRouteResponse>("/api/login", this);
    }
}
