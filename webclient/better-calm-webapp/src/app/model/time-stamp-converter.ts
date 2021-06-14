import { TimeStamp } from "./time-stamp"

export class TimeStampConverter{
    public static GetDomainTimeStamp(ts:TimeStamp):string{
        let hours = `${ts.hours}`;
        if (ts.hours < 10){
            hours = `0${ts.hours}`;
        }
        if (ts.hours == 0){
            hours = `0${ts.hours}`;
        }
        let minutes = `${ts.minutes}`;
        if (ts.minutes < 10){
            minutes = `0${ts.minutes}`;
        }
        if (ts.minutes == 0){
            minutes = `0${ts.minutes}`;
        }
        let seconds = `${ts.seconds}`;
        if (ts.minutes < 10){
            seconds = `0${ts.seconds}`;
        }
        if (ts.seconds == 0){
            seconds = `0${ts.seconds}`;
        }
        return `${hours}:${minutes}:${seconds}`;
    }
}