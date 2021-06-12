import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Playlist } from 'src/app/domain/playlist';
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

  private convertBasicInfoPlaylists(basicInfo:PlaylistBasicInfo[]):Playlist[]{
    return basicInfo.map(basic => PlaylistBasicInfoConverter.GetDomainPlaylist(basic));
  }
}
