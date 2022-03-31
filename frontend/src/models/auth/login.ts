export interface ILoginData {
    username: string;
    passwd: string;
    errors: Array<string>;
}

export class LoginData {
    username: string;
    passwd: string;
    errors: Array<string>;

    constructor() {
        this.username = "";
        this.passwd = "";
        this.errors = new Array<string>();
    }
}
