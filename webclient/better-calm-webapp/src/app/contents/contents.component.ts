import { Component, OnInit } from '@angular/core';
import { ContentsService } from '../services/contents/contents.service';

@Component({
  selector: 'app-contents',
  templateUrl: './contents.component.html',
  styleUrls: ['./contents.component.scss']
})
export class ContentsComponent implements OnInit {
  contents:any[] = [];
  stringContents:string = "";
  constructor(private contentsService: ContentsService) { }

  ngOnInit(): void {
    this.contentsService.getAll().subscribe((contents) => this.setContents(contents), console.error);
  }

  private setContents(contents:any[]){
    this.contents = contents;
    this.stringContents = JSON.stringify(this.contents);
  }
}
