import { Component, OnInit } from '@angular/core';
import { Content } from '../domain/content';
import { ContentsService } from '../services/contents/contents.service';

@Component({
  selector: 'app-contents',
  templateUrl: './contents.component.html',
  styleUrls: ['./contents.component.scss']
})
export class ContentsComponent implements OnInit {
  contents:Content[] = [];
  obtainedContents:Content[] = [];
  filter:string[] = [ "audio", "video"]
  isLoading = true;

  constructor(private contentsService: ContentsService) { }

  ngOnInit(): void {
    this.contentsService.getAll().subscribe((contents) => this.setContents(contents), console.error);
  }

  private setContents(contents:Content[]){
    this.obtainedContents = contents;
    this.setShowContents();;
  }

  private setShowContents():void{
    this.isLoading = true;
    this.contents = [];
    this.obtainedContents.forEach((content) => {
      if (this.filter.includes(content.contentType))
        this.contents.push(content);
    })
    this.isLoading = false;
  }

  public toogleFilterValue(filterValue:string):void{
    if (this.filter.includes(filterValue)){
      let index = this.filter.indexOf(filterValue);
      this.filter.splice(index, 1);
    }
    else{
      this.filter.push(filterValue);
    }
    this.setShowContents();
  }
}
