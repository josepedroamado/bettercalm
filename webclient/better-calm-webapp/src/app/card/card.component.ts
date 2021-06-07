import { Component, Injectable, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})

export class CardComponent implements OnInit {
  @Input() name:string = "";
  @Input() target:string = ""
  constructor() { }

  ngOnInit(): void {
  }

}
