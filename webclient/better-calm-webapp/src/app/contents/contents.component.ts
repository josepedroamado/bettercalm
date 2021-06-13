import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Category } from '../domain/category';
import { Content } from '../domain/content';
import { CategoriesService } from '../services/categories/categories.service';
import { ContentsService } from '../services/contents/contents.service';
import { PlaylistsService } from '../services/playlists/playlists.service';

@Component({
  selector: 'app-contents',
  templateUrl: './contents.component.html',
  styleUrls: ['./contents.component.scss']
})
export class ContentsComponent implements OnInit {
  contents:Content[] = [];
  categories:Category[] = [];
  obtainedContents:Content[] = [];
  typeFilters:string[] = [ "audio", "video"]
  categoryFilters:number[] = [];
  isLoading = true;
  playlistId:number | undefined;

  constructor(private contentsService: ContentsService, 
    private categoriesService:CategoriesService,
    private playlistsService:PlaylistsService, 
    private currentRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.playlistId = this.currentRoute.snapshot.params['playlistId'];
    this.contentsService.contentRemoved.subscribe(() => this.getContents());
    this.getContents();
    this.getCategories();
  }

  private getContents(){
    if (this.playlistId){
      this.playlistsService.getContents(this.playlistId).subscribe((contents) => this.setContents(contents), console.error);
    }
    else{
      this.contentsService.getAll().subscribe((contents) => this.setContents(contents), console.error);
    }    
  }

  private getCategories(){
    this.categoriesService.getAll().subscribe((categories) => this.setCategories(categories), console.error);
  }

  private setContents(contents:Content[]){
    this.obtainedContents = contents;
    this.setShowContents();;
  }

  private setCategories(categories:Category[]){
    this.categories = categories;
    this.categoryFilters = [];
    this.categories.forEach((category => this.categoryFilters.push(category.id)));
    this.setShowContents();
  }

  private setShowContents():void{
    this.isLoading = true;
    this.contents = [];
    this.obtainedContents.forEach((content) => {
      if (this.typeFilters.includes(content.contentType) && 
        content.categories?.some(category => this.categoryFilters.includes(category)))
        this.contents.push(content);
    })
    this.isLoading = false;
  }

  public switchContentTypeValue(filterValue:string):void{
    if (this.typeFilters.includes(filterValue)){
      let index = this.typeFilters.indexOf(filterValue);
      this.typeFilters.splice(index, 1);
    }
    else{
      this.typeFilters.push(filterValue);
    }
    this.setShowContents();
  }

  public switchCategoryValue(id:number):void{
    if (this.categoryFilters.includes(id)){
      let index = this.categoryFilters.indexOf(id);
      this.categoryFilters.splice(index, 1);
    }
    else{
      this.categoryFilters.push(id);
    }
    this.setShowContents();
  }

  private removeContent(id:number):void{

  }
}
