import { Component, OnInit, Input } from '@angular/core';
import { Content } from '../../../domain/content';
import { DomSanitizer } from '@angular/platform-browser';


@Component({
  selector: 'app-content-detail-video',
  templateUrl: './content-detail-video.component.html',
  styleUrls: ['./content-detail-video.component.scss']
})
export class ContentDetailVideoComponent implements OnInit {
  @Input() content:Content | any= {};

  constructor(private sanitizer:DomSanitizer) { }

  ngOnInit(): void {
  }

  getContentUrl(){
    return this.sanitizer.bypassSecurityTrustResourceUrl(this.content.contentUrl);
  }

}
