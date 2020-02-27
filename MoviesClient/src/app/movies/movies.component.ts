import { Component, OnInit } from '@angular/core';
import { ApiWrapperService } from '../api-wrapper.service';
import { Movie } from '../model/Movie';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {
  public movies: Movie[];
  constructor(private apiWrapper: ApiWrapperService) { }

  ngOnInit() {
    this.apiWrapper.getMovies().subscribe(i => this.movies = i);
  }

}
