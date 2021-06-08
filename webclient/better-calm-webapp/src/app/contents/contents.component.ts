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
  stringContents:string = "";
  constructor(private contentsService: ContentsService) { }

  ngOnInit(): void {
    this.contentsService.getAll().subscribe((contents) => this.setContents(contents), console.error);
  }

  private setContents(contents:Content[]){
    this.contents = contents;
    this.stringContents = JSON.stringify(this.contents);
  }
}
