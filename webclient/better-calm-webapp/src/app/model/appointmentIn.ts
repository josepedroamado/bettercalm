export class AppointmentIn {
    psychologistName: string;
    format: string;
    address: string;
    date: Date;

    constructor(psychologistName:string = "", format:string = "", address:string = "", date:Date = new Date) {
        this.psychologistName = psychologistName;
        this.format = format;
        this.address = address;
        this.date = date;
    }
}