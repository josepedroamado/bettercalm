import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../../../domain/category';
import { Content } from '../../../domain/content';
import { Playlist } from '../../../domain/playlist';
import { CategoriesService } from '../../../services/categories/categories.service';
import { ContentsService } from '../../../services/contents/contents.service';
import { PlaylistsService } from '../../../services/playlists/playlists.service';

@Component({
  selector: 'app-content-edit',
  templateUrl: './content-edit.component.html',
  styleUrls: ['./content-edit.component.scss']
})
export class ContentEditComponent implements OnInit {
  private static readonly urlPattern = '(http|https):\/\/.*';
  private static readonly timeSpanPattern = '[0-9][0-9]:[0-5][0-9]:[0-5][0-9]';
  public isCreate:boolean = true;
  public content:Content = {} as any;
  public categories:Category[] = [];
  public selectedCategories: number[] = [];
  public playlists:Playlist[] = [];
  public selectedPlaylists: number[] = [];
  public isLoadingContent:boolean = true;
  public isLoadingCategories:boolean = true;
  public hasError:boolean = false;
  public errorMessage: string = "";
  public contentForm = this.formBuilder.group(
    {
      id: [''],
      name: ['', Validators.required],
      contentLength: ['', [Validators.pattern(ContentEditComponent.timeSpanPattern), Validators.required]],
      artistName: ['', [Validators.required]],
      imageUrl: ['', [Validators.pattern(ContentEditComponent.urlPattern)]],
      contentUrl: ['', [Validators.pattern(ContentEditComponent.urlPattern), Validators.required]],
      contentType: ['audio', Validators.required],
      categories: ['', Validators.required],
      playlistIds: ['']
    });
  
  public playlistForm = this.formBuilder.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    playlistImageUrl: ['', [Validators.pattern(ContentEditComponent.urlPattern)]],
    categories: ['', Validators.required],
  });

  constructor(private contentsService: ContentsService,
    private categoriesService: CategoriesService,
    private playlistsService: PlaylistsService,
    private currentRoute: ActivatedRoute, 
    private formBuilder: FormBuilder,
    private router: Router) { }
  
  ngOnInit(): void {
    this.getContent();
    this.getCategories();
    this.getPlaylists();
  }

  private getContent():void{
    this.isLoadingContent = true;
    let id:string = this.currentRoute.snapshot.params['id'];
    if (id){
      this.isCreate = false;
      this.contentsService.get(id).subscribe((content) => this.setContent(content), this.setError.bind(this));
    }
    else{
      this.isLoadingContent = false;
    }
  }

  private getCategories():void{
    this.isLoadingCategories = true;
    this.categoriesService.getAll().subscribe((categories) => this.setCategories(categories), this.setError.bind(this));
  }

  private getPlaylists():void{
    this.playlistsService.getAll().subscribe((playlists) => this.setPlaylists(playlists), (error) => console.log(error));
  }

  private setContent(content:Content):void{
    this.selectedCategories = content.categories;
    this.selectedPlaylists = content.playlistIds;
    this.content = content;
    this.contentForm.patchValue({
      id: content.id,
      name: content.name,
      contentLength: content.contentLength,
      artistName: content.artistName,
      imageUrl: content.imageUrl,
      contentUrl: content.contentUrl,
      contentType: content.contentType,
      playlistIds: content.playlistIds
    })
    this.hasError = false;
    this.isLoadingContent = false;
  }

  private setCategories(categories:Category[]):void{
    this.categories = categories;
    this.isLoadingCategories = false;
  }

  private setPlaylists(playlists:Playlist[]):void{
    this.playlists = playlists;
  }

  onSubmit(content: Content){
    content.playlists = [];
    if (content.playlistIds?.length > 0){
      this.playlists.map(playlist => {
        if (content.playlistIds.includes(playlist.id)){
          content.playlists.push(playlist);
        }
      })
    }
    
    if (this.isCreate){
      content.id = 0;
      this.contentsService.post(content).subscribe(
        () => this.setOk(), 
        (error) => this.setError(error));
    }
    else{
      this.contentsService.patch(content).subscribe(
        () => this.setOk(), 
        (error) => this.setError(error));
    }    
  }

  onSubmitPlaylist(playlist: Playlist){
    playlist.id = 0;
    let currentContent = this.contentForm.value;
    currentContent.playlistIds.push(playlist.id);
    let updatedPlaylists = this.playlists;
    updatedPlaylists.push(playlist);
    this.playlists = [...updatedPlaylists];
    this.contentForm.patchValue(currentContent);
  }

  private setError(error:string){
    this.hasError = true;
    this.errorMessage = error;
    console.log(error);
  }

  private setOk():void{
    alert("Enviado correctamente!");
    this.router.navigate(['/admin/contents']);
  }
}