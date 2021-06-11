import { HttpClient } from "@angular/common/http";
import { throwError } from "rxjs";

export class BaseService{
    constructor(public http: HttpClient) { }
    
    public handleError(errorRequest: any) {
      console.error(errorRequest);
      return throwError(errorRequest.error || errorRequest.message);
    }
}