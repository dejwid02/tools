import { Component, OnInit } from '@angular/core';
import { ApiWrapperService } from '../api-wrapper.service';
import { Recording } from '../model/Recording';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-recording-details',
  templateUrl: './recording-details.component.html',
  styleUrls: ['./recording-details.component.css']
})
export class RecordingDetailsComponent implements OnInit {

  constructor(private apiWrapper: ApiWrapperService, private router: Router, private route: ActivatedRoute) { }
  public recording: Recording = {id: 0, movie: {id: 0, category: '', description: '', title: '', imageUrl: ''}, recordedAtTime: new Date()};
  id: number;
  ngOnInit() {
    this.id = +this.route.snapshot.paramMap.get('id');
    if (this.id === 0) {
      const movieId = +this.route.snapshot.paramMap.get('movieId');
      this.apiWrapper.getMovie(movieId).subscribe(m => this.recording.movie = m);
      const date = new Date(this.route.snapshot.paramMap.get('startTime'));
      this.recording.recordedAtTime = date;
    }
  }

  public save(): void {
    this.apiWrapper.createRecording(this.recording);
    this.router.navigateByUrl('/recordings');
  }

}
