import { Component, OnInit } from '@angular/core';
import { Content } from '../../../domain/content';
import { ContentsService } from '../../../services/contents/contents.service';

@Component({
  selector: 'app-content-management',
  templateUrl: './content-management.component.html',
  styleUrls: ['./content-management.component.scss']
})
export class ContentManagementComponent implements OnInit {
  contents:Content[] = [];
  isLoading = true;
  error = false;

  constructor(private contentsService: ContentsService){}

  ngOnInit(): void {
    this.getContents();
  }

  private getContents(){
    this.contentsService.getAll().subscribe((contents) => this.setContents(contents), (error : any) => this.showError(error));
  }

  private showError(error: any){
    this.error = true;
    this.isLoading = false;
  }

  private setContents(contents:Content[]){
    this.contents = contents;
    this.isLoading = false;
  }

  public removeContent(id:number):void{
    this.contentsService.delete(id).subscribe(() => this.getContents());
  }
}
