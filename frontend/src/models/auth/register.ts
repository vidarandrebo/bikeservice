export interface IRegisterData {
    username: string;
    passwd: string;
    repasswd: string;
    error: Array<string>;

    passwordRequirementsCheck(): void;
}

export class RegisterData {
    username: string;
    passwd: string;
    repasswd: string;
    error: Array<string>;

    passwordRequirementsCheck(): void {
        this.error = new Array<string>();
        if (this.passwd !== this.repasswd) {
            this.error.push("Passwords do not match");
        }
        if (this.passwd.length < 8) {
            this.error.push("Password should be 8 characters or longer!");
        }
    }

    constructor() {
        this.username = "";
        this.passwd = "";
        this.repasswd = "";
        this.error = new Array<string>();
    }

}