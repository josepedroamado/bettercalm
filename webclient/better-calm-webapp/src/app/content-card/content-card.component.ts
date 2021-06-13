import { Component, OnInit, Input } from '@angular/core';
import { Content } from '../domain/content';
import { ContentsService } from '../services/contents/contents.service';

@Component({
  selector: 'app-content-card',
  templateUrl: './content-card.component.html',
  styleUrls: ['./content-card.component.scss']
})
export class ContentCardComponent implements OnInit {
  @Input() content:Content | any= {};
  detailContentUri:string = `contents/detail`;
  showDetail:boolean = false;
  contentsBasePath = "contents/";
  editPath = "edit";

  
  constructor(private contentsService: ContentsService) { }

  ngOnInit(): void {
  }

  public getDetailsUri():string{
    return this.detailContentUri+'?id='+this.content.id;
  }

  public show():void{
    this.showDetail = !this.showDetail;
  }

  public getEditPath():string{
    return this.contentsBasePath+this.editPath+`?id=${this.content.id}`;
  }

  public removeContent(id:number):void{
    this.contentsService.delete(id).subscribe();
  }
}
