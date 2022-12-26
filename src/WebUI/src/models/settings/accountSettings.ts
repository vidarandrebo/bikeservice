export interface IAccountSettings {
    oldPassword: string;
    newPassword: string;
    reNewPassword: string;
    errors: string[];
}

export class AccountSettings implements IAccountSettings{
    oldPassword: string;
    newPassword: string;
    reNewPassword: string;
    errors: string[];

    constructor() {
        this.oldPassword = "";
        this.newPassword = "";
        this.reNewPassword = "";
        this.errors = new Array<string>();
    }
}