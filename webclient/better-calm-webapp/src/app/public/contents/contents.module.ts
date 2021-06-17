import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentCardComponent } from './content-card/content-card.component';
import { ContentDetailComponent } from './content-detail/content-detail.component';
import { ContentDetailAudioComponent } from './content-detail-audio/content-detail-audio.component';
import { ContentDetailVideoComponent } from './content-detail-video/content-detail-video.component';
import { UtilsModule } from 'src/app/utils/utils.module';
import { ContentsComponent } from './contents/contents.component';
import { CategoriesComponent } from './categories/categories.component';



@NgModule({
  declarations: [
    ContentCardComponent,
    ContentDetailComponent,
    ContentDetailAudioComponent,
    ContentDetailVideoComponent,
    ContentsComponent,
    CategoriesComponent
  ],
  imports: [
    CommonModule,
    UtilsModule
  ],
  exports: [
    ContentCardComponent,
    ContentDetailComponent,
    ContentDetailAudioComponent,
    ContentDetailVideoComponent,
    ContentsComponent,
    CategoriesComponent
  ]
})
export class ContentsModule { }
