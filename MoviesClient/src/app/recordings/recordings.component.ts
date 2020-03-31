import { Component, OnInit } from '@angular/core';
import { Recording } from '../model/Recording';
import { ApiWrapperService } from '../api-wrapper.service';

@Component({
  selector: 'app-recordings',
  templateUrl: './recordings.component.html',
  styleUrls: ['./recordings.component.css']
})
export class RecordingsComponent {
recordings$ = this.apiWrapper.recordings$;
constructor(private apiWrapper: ApiWrapperService) { }

  public delete(id: number): void {
    this.apiWrapper.deleteRecording(id);
  }

}
