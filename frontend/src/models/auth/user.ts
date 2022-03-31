import {fetchResponse, postWithBody} from "@/models/genericFetch";
import {AuthResponse} from "@/models/auth/authResponse";

export interface IUser {
    userName: string;
    password: string;

    registerUserRequest(): Promise<fetchResponse<AuthResponse>>;

    loginUserRequest(): Promise<fetchResponse<AuthResponse>>;
}

export class User {
    userName: string;
    password: string;

    constructor(uname: string, passwd: string) {
        this.userName = uname;
        this.password = passwd;
    }

    async registerUserRequest(): Promise<fetchResponse<AuthResponse>> {
        return await postWithBody<IUser, AuthResponse>("/register", this);
    }

    async loginUserRequest(): Promise<fetchResponse<AuthResponse>> {
        return await postWithBody<IUser, AuthResponse>("/login", this);
    }


}