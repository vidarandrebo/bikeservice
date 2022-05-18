import {FetchResponse, postWithBody} from "@/models/genericFetch";
import {AuthRouteResponse} from "@/models/auth/authRouteResponse";

export interface IUser {
    userName: string;
    password: string;

    registerUserRequest(): Promise<FetchResponse<AuthRouteResponse>>;

    loginUserRequest(): Promise<FetchResponse<AuthRouteResponse>>;
}

export class User implements IUser {
    userName: string;
    password: string;

    constructor(uname: string, passwd: string) {
        this.userName = uname;
        this.password = passwd;
    }

    async registerUserRequest(): Promise<FetchResponse<AuthRouteResponse>> {
        return await postWithBody<IUser, AuthRouteResponse>("/register", this);
    }

    async loginUserRequest(): Promise<FetchResponse<AuthRouteResponse>> {
        return await postWithBody<IUser, AuthRouteResponse>("/login", this);
    }


}