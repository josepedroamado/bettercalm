import { Component, OnInit, Input } from '@angular/core';
import { Content } from '../domain/content';

@Component({
  selector: 'app-content-detail-audio',
  templateUrl: './content-detail-audio.component.html',
  styleUrls: ['./content-detail-audio.component.scss']
})
export class ContentDetailAudioComponent implements OnInit {
  @Input() content:Content | any= {};
  constructor() { }

  ngOnInit(): void {
  }

}
