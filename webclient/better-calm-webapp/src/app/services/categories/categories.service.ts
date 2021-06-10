import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { BaseService } from '../common/base-service';
import { Category } from 'src/app/domain/category';
import { ModelCategory } from 'src/app/model/model-category';
import { ModelCategoryConverter } from 'src/app/model/model-category-converter';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService extends BaseService{
  private readonly target_url:string = `${environment.api_url}/categories`;

  constructor(http: HttpClient) {
    super(http);
  }

  public getAll(): Observable<Category[]> {
    return this.http
      .get<ModelCategory[]>(this.target_url)
      .pipe(catchError(this.handleError), map(this.convertBasicInfoContents));
  }

  private convertBasicInfoContents(modelCategories:ModelCategory[]):Category[]{
    return modelCategories.map(modelCategory => ModelCategoryConverter.GetDomainCategory(modelCategory));
  }
}
