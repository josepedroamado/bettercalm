import { Component, OnInit, Input } from '@angular/core';
import { Content } from '../domain/content';

@Component({
  selector: 'app-content-detail',
  templateUrl: './content-detail.component.html',
  styleUrls: ['./content-detail.component.scss']
})
export class ContentDetailComponent implements OnInit {
  @Input() content:Content = {} as any;
  
  constructor() { 
  }

  ngOnInit() {}

}
