import { Component, OnInit } from '@angular/core';
import { Playlist } from '../../domain/playlist';
import { PlaylistsService } from '../../services/playlists/playlists.service';

@Component({
  selector: 'app-playlists',
  templateUrl: './playlists.component.html',
  styleUrls: ['./playlists.component.scss']
})
export class PlaylistsComponent implements OnInit {
  isLoading:boolean = false;
  playlists:Playlist[] = [];
  obtainedPlaylists:Playlist[] = [];
  selectedCategories:number[] = [];
  hasError:boolean = false;
  errorMessage:string = "";
  
  constructor(private playlistsService:PlaylistsService) { }

  ngOnInit(): void {
    this.getPlaylists();
  }

  private getPlaylists():void{
    this.playlistsService.getAll().subscribe((playlists) => this.setPlaylists(playlists), this.setError.bind(this))
  }

  private setPlaylists(playlists:Playlist[]):void{
    this.obtainedPlaylists = playlists;
    this.showPlaylists();
    this.isLoading = false;
  }

  private setError(error:string){
    this.hasError = true;
    this.errorMessage = error;
    console.log(error);
  }

  public changeSelectedCatagories(selectedCategories:number[]):void{
    this.selectedCategories = selectedCategories;
    this.showPlaylists();
  }

  private showPlaylists():void{
    this.playlists = [];
    this.obtainedPlaylists.forEach(playlist => {
      if (playlist.categories.length == 0 || 
        playlist.categories?.some(category => this.selectedCategories.includes(category)))
        this.playlists.push(playlist);
    })
  }
}
