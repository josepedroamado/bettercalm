import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Content } from 'src/app/domain/content';
import { Playlist } from 'src/app/domain/playlist';
import { ContentBasicInfo } from 'src/app/model/content-basic-info';
import { ContentBasicInfoConverter } from 'src/app/model/content-basic-info-converter';
import { PlaylistBasicInfo } from 'src/app/model/playlist-basic-info';
import { PlaylistBasicInfoConverter } from 'src/app/model/playlist-basic-info-converter';
import { environment } from 'src/environments/environment';
import { BaseService } from '../common/base-service';

@Injectable({
  providedIn: 'root'
})
export class PlaylistsService extends BaseService {
  private readonly target_url:string = `${environment.api_url}/playlists`
  
  constructor(http:HttpClient) {
    super(http);
  }

  public getAll(): Observable<Playlist[]> {
    return this.http
      .get<PlaylistBasicInfo[]>(this.target_url)
      .pipe(catchError(this.handleError), map(this.convertBasicInfoPlaylists));
  }

  public getContents(id:number): Observable<Content[]> {
    let url = this.target_url+'/'+id+'/contents';
    return this.http
      .get<ContentBasicInfo[]>(url)
      .pipe(catchError(this.handleError), map(this.convertBasicInfoContents));
  }

  private convertBasicInfoPlaylists(basicInfo:PlaylistBasicInfo[]):Playlist[]{
    return basicInfo.map(basic => PlaylistBasicInfoConverter.GetDomainPlaylist(basic));
  }

  private convertBasicInfoContents(contentsBasicInfo:ContentBasicInfo[]):Content[]{
    return contentsBasicInfo.map(content => ContentBasicInfoConverter.GetDomainContent(content));
  }
}
