import { FetchResponse, httpPostWithBody } from "../HttpMethods.ts";
import { AuthRouteResponse } from "./AuthRouteResponse.ts";

export interface ICredentials {
    userName: string;
    password: string;

    registerUserRequest(): Promise<FetchResponse<AuthRouteResponse>>;

    loginUserRequest(): Promise<FetchResponse<AuthRouteResponse>>;
}

export class Credentials implements ICredentials {
    userName: string;
    password: string;

    constructor(uname: string, passwd: string) {
        this.userName = uname;
        this.password = passwd;
    }

    async registerUserRequest(): Promise<FetchResponse<AuthRouteResponse>> {
        return await httpPostWithBody<ICredentials, AuthRouteResponse>("/api/register", this);
    }

    async loginUserRequest(): Promise<FetchResponse<AuthRouteResponse>> {
        return await httpPostWithBody<ICredentials, AuthRouteResponse>("/api/login", this);
    }
}
