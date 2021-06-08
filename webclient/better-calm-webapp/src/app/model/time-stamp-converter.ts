import { TimeStamp } from "./time-stamp"

export class TimeStampConverter{
    public static GetDomainTimeStamp(ts:TimeStamp):string{
        return `${ts.hours}:${ts.minutes}:${ts.seconds}`;
    }
}