import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Category } from '../domain/category';
import { Content } from '../domain/content';
import { ContentsService } from '../services/contents/contents.service';
import { PlaylistsService } from '../services/playlists/playlists.service';

@Component({
  selector: 'app-contents',
  templateUrl: './contents.component.html',
  styleUrls: ['./contents.component.scss']
})
export class ContentsComponent implements OnInit {
  contents:Content[] = [];
  selectedCategories:number[] = [];
  obtainedContents:Content[] = [];
  typeFilters:string[] = [ "audio", "video"]
  isLoading = true;
  playlistId:number | undefined;

  constructor(private contentsService: ContentsService, 
    private playlistsService:PlaylistsService, 
    private currentRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.playlistId = this.currentRoute.snapshot.params['playlistId'];
    this.contentsService.contentRemoved.subscribe(() => this.getContents());
    this.getContents();
  }

  private getContents(){
    if (this.playlistId){
      this.playlistsService.getContents(this.playlistId).subscribe((contents) => this.setContents(contents), console.error);
    }
    else{
      this.contentsService.getAll().subscribe((contents) => this.setContents(contents), console.error);
    }    
  }

  private setContents(contents:Content[]){
    this.obtainedContents = contents;
    this.setShowContents();;
  }

  public changeSelectedCatagories(selectedCategories:number[]):void{
    this.selectedCategories = selectedCategories;
    this.setShowContents();
  }

  private setShowContents():void{
    this.isLoading = true;
    this.contents = [];
    this.obtainedContents.forEach((content) => {
      if (this.typeFilters.includes(content.contentType) && 
        content.categories?.some(category => this.selectedCategories.includes(category)))
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
}
