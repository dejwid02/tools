import { Component, OnInit } from '@angular/core';
import { Recording } from '../model/Recording';
import { ApiWrapperService } from '../api-wrapper.service';

@Component({
  selector: 'app-recordings',
  templateUrl: './recordings.component.html',
  styleUrls: ['./recordings.component.css']
})
export class RecordingsComponent implements OnInit {

  constructor(private apiWrapper: ApiWrapperService) { }
public recordings: Recording[];
  ngOnInit() {
    this.apiWrapper.getRecordings().subscribe(i=>this.recordings=i)
  }

}
