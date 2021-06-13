import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { throwError } from "rxjs";

@Injectable()
export class BaseService{
    constructor(public http: HttpClient) { }
    
    public handleError(errorRequest: any) {
      console.error(errorRequest);
      return throwError(errorRequest.error || errorRequest.message);
    }

  public getAuthOptions(){
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': localStorage.getItem("token") ?? ""
      })
    };
  }
}