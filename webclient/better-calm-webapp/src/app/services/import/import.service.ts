import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ImportInfo } from 'src/app/domain/import-info';
import { ImportTypes } from 'src/app/domain/import-types';
import { ModelImportInfo } from 'src/app/model/model-import-info';
import { ModelImportInfoConverter } from 'src/app/model/model-import-info-converter';
import { ModelImportTypes } from 'src/app/model/model-import-types';
import { ModelImportTypesConverter } from 'src/app/model/model-import-types-converter';
import { environment } from 'src/environments/environment';
import { BaseService } from '../common/base-service';

@Injectable({
  providedIn: 'root'
})
export class ImportService extends BaseService{
  private readonly target_url:string = `${environment.api_url}/importers`;

  constructor(http: HttpClient) {
    super(http);
  }

  public post(importInfo:ImportInfo):Observable<ImportInfo>{
    let modelImportInfo:ModelImportInfo = ModelImportInfoConverter.GetModelImportInfo(importInfo);
    return this.http
      .post<ModelImportInfo>(this.target_url, modelImportInfo, this.getAuthOptions())
      .pipe(catchError(this.handleError), map(() => importInfo));
  }

  public getTypes(): Observable<ImportTypes> {
    let url:string = this.target_url+'/types';
    return this.http
      .get<ModelImportTypes>(url, this.getAuthOptions())
      .pipe(catchError(this.handleError), map(this.convertModelImportTypes));
  }

  private convertModelImportTypes(model:ModelImportTypes):ImportTypes{
    return ModelImportTypesConverter.GetDomainImportTypes(model);
  }
}
