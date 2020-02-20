import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiWrapperService } from '../api-wrapper.service';
import { TvListingItem } from '../model/TvListingItem';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  tvItems: TvListingItem[];
  constructor(private apiWrapper: ApiWrapperService) { }

  ngOnInit() {
    this.apiWrapper.getTvListingItems().subscribe(i => this.tvItems = i);
  }

}
