export interface IRegisterData {
    username: string;
    passwd: string;
    repasswd: string;
    error: Array<string>;
    passwordRequirementsCheck() : void;
}

export class RegisterData {
    username: string;
    passwd: string;
    repasswd: string;
    error: Array<string>;
    passwordRequirementsCheck() : void {
        this.error = new Array<string>();
        if (this.passwd !== this.repasswd) {
            this.error.push("Passwords do not match");
        }
        if (this.passwd.length < 8) {
            this.error.push("Password should be 8 characters or longer!");
        }
    }
}

export interface IUser {
    userName: string;
    password: string;
    date: Date;
    registerUserRequest() : Promise<number>;
}
export class User {
    userName: string;
    password: string;
    date: Date;
    async registerUserRequest() : Promise<number> {
        let response = await fetch('/Register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(this)
        });
        console.log(JSON.stringify(this));
        return response.status;
    }
}
