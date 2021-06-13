import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { throwError } from "rxjs";

@Injectable()
export class BaseService{
  public authOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': localStorage.getItem("token") ?? ""
    })
  };
  
    constructor(public http: HttpClient) { }
    
    public handleError(errorRequest: any) {
      console.error(errorRequest);
      return throwError(errorRequest.error || errorRequest.message);
    }
}