export class AppointmentOut {
    illnessId: number;
    name: string;
    lastName: string;
    birthDate: Date;
    email: string;
    phone: string;
    duration: string;

    constructor(illnessId:number = -1, 
                name:string = "", 
                lastName:string = "", 
                birthDate:Date = new Date,
                email:string = "", 
                phone:string = "", 
                duration:string = "") { 
        this.illnessId = +illnessId;
        this.name = name;
        this.lastName = lastName;
        this.birthDate = birthDate;
        this.email = email;
        this.phone = phone;
        this.duration = duration;
    }
}