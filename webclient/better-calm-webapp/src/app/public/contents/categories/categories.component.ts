import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Category } from '../../../domain/category';
import { CategoriesService } from '../../../services/categories/categories.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent implements OnInit {
  categories:Category[] = [];
  selectedCategories:number[] = [];
  isLoading:boolean = false;

  @Output() changeSelectedCategories = new EventEmitter<number[]>();

  constructor(private categoriesService:CategoriesService) { }

  ngOnInit(): void {
    this.getCategories();
  }

  private getCategories():void{
    this.isLoading = true;
    this.categoriesService.getAll().subscribe((categories) => this.setCategories(categories));
  }

  private setCategories(categories:Category[]):void{
    this.categories = categories;
    this.selectedCategories = [];
    this.categories.forEach(category => this.selectedCategories.push(category.id));
    this.isLoading = false;
    this.changeSelectedCategories.emit(this.selectedCategories);
  }

  public switchCategoryValue(id:number):void{
    if (this.selectedCategories.includes(id)){
      let index = this.selectedCategories.indexOf(id);
      this.selectedCategories.splice(index, 1);
    }
    else{
      this.selectedCategories.push(id);
    }
    this.changeSelectedCategories.emit(this.selectedCategories);
  }

}
