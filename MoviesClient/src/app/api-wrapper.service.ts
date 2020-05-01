import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {catchError, tap} from 'rxjs/operators';
import { TvListingItem } from './model/TvListingItem';
import { Recording } from './model/Recording';
import { Movie } from './model/Movie';
@Injectable({
  providedIn: 'root'
})
export class ApiWrapperService {

  tvListingUrl: string;
  recordingsUrl: string;
  moviesUrl: string;
  tvListingItems$ : Observable<TvListingItem[]>;
  recordings$ : Observable<Recording[]>;
  movies$ : Observable<Movie[]>;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.tvListingUrl = baseUrl + '/api/tvitems?hidePast=true&hiderecorded=true';
    this.recordingsUrl = baseUrl + '/api/recordings';
    this.moviesUrl = baseUrl + '/api/movies';
    this.tvListingItems$ =  this.http.get<TvListingItem[]>(this.tvListingUrl);
    this.recordings$ = this.http.get<Recording[]>(this.recordingsUrl);
    this.movies$ = this.http.get<Movie[]>(this.moviesUrl);
  }

  public getMovie(id: number): Observable<Movie> {

    return this.http.get<Movie>(this.moviesUrl + '/' + id);
  }

  public createRecording(recording: Recording ): void {

    this.http.post(this.recordingsUrl, recording).subscribe();
  }

  public deleteRecording(id: number) {

     this.http.delete(this.recordingsUrl + '/' + id).subscribe();
  }

}
