export class LoginOut {
    eMail: string;
    password: string;

    constructor(eMail:string = "", password:string = "") {
        this.eMail = eMail;
        this.password = password;
    }
}