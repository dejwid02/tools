import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiWrapperService } from '../api-wrapper.service';
import { TvListingItem } from '../model/TvListingItem';
import {Observable, Subject, Subscription} from 'rxjs';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(private apiWrapper: ApiWrapperService) { }
  public tvItems$ = this.apiWrapper.tvListingItemsAggregated$;

}
