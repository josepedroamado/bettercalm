import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { throwError } from "rxjs";

export class BaseService{
    constructor(public http: HttpClient) { }
    
    public handleError(error: HttpErrorResponse) {
        if (error.status === 0) {
          return throwError("Server is shut down");
        }
        else {
          return throwError(
            error.error ? error.error.message : "Server problems, try later");
        }
      }
}