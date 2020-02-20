import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {catchError, tap} from 'rxjs/operators';
import { TvListingItem } from './model/TvListingItem';
@Injectable({
  providedIn: 'root'
})
export class ApiWrapperService {

  tvListingUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.tvListingUrl = baseUrl + '/api/tvitems';
  }

  public getTvListingItems(): Observable<TvListingItem[]> {
    console.log("calling" + this.tvListingUrl);
    return this.http.get<TvListingItem[]>(this.tvListingUrl);
  }


}
