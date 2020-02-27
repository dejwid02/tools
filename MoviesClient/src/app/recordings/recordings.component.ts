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
    this.loadRecordings();
  }

  public delete(id: number): void {
    this.apiWrapper.deleteRecording(id);
    this.recordings.length = 0;
    this.apiWrapper.getRecordings().subscribe(i => this.recordings.concat(i));
    this.loadRecordings();
  }

  loadRecordings(): void {
    this.apiWrapper.getRecordings().subscribe(i => this.recordings = i);
  }

}
