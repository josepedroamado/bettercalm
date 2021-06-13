import { Component, OnInit, Input } from '@angular/core';
import { Content } from '../../../domain/content';

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

  
  constructor() { }

  ngOnInit(): void {
  }

  public getDetailsUri():string{
    return this.detailContentUri+'?id='+this.content.id;
  }

  public show():void{
    this.showDetail = !this.showDetail;
  }
}
