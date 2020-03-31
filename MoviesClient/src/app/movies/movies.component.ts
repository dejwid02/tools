import { Component, OnInit } from '@angular/core';
import { ApiWrapperService } from '../api-wrapper.service';
import { Movie } from '../model/Movie';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent {
  public movies$ = this.apiWrapper.movies$;
  constructor(private apiWrapper: ApiWrapperService) { }
}
