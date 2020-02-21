import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {catchError, tap} from 'rxjs/operators';
import { TvListingItem } from './model/TvListingItem';
import { Recording } from './model/Recording';
@Injectable({
  providedIn: 'root'
})
export class ApiWrapperService {

  tvListingUrl: string;
  recordingsUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.tvListingUrl = baseUrl + '/api/tvitems?hidePast=false';
    this.recordingsUrl = baseUrl + '/api/recordings';
  }

  public getTvListingItems(): Observable<TvListingItem[]> {
    return this.http.get<TvListingItem[]>(this.tvListingUrl);
  }

  public getRecordings(): Observable<Recording[]> {
    return this.http.get<Recording[]>(this.recordingsUrl);
  }

  public createRecording(tvItem: TvListingItem): void {
    const recording: Recording =  {
      id:  0,
      movie: tvItem.movie,
      recordedAtTime: tvItem.startTime
    };
    this.http.post(this.recordingsUrl, recording).subscribe();
  }

}
