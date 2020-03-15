import { Component, OnInit } from '@angular/core';
import { ApiWrapperService } from '../api-wrapper.service';
import { ActivatedRoute } from '@angular/router';
import { Movie } from '../model/Movie';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {
public movie: Movie;
  constructor(private apiWrapper: ApiWrapperService, private route: ActivatedRoute) { }

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.apiWrapper.getMovie(id).subscribe(m => this.movie = m);
  }

}
