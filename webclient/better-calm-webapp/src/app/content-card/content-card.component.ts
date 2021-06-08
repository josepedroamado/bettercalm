import { Component, OnInit, Input } from '@angular/core';
import { Content } from '../domain/content';

@Component({
  selector: 'app-content-card',
  templateUrl: './content-card.component.html',
  styleUrls: ['./content-card.component.scss']
})
export class ContentCardComponent implements OnInit {
  @Input() content:Content | any= {};
  playContentUri:string = `contents/`;
  
  constructor() { 
    this.playContentUri += this.content.id;
  }

  ngOnInit(): void {
  }

}
