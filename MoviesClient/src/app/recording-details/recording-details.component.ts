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
  public recording: Recording = {id: 0, movie: {id: 0, category: '', description: '', title: '', imageUrl: '', country: '', rating: 0, year: 0},
   recordedAtTime: new Date()};
  id: number;
  ngOnInit() {
    this.id = +this.route.snapshot.paramMap.get('id');
    if (this.id === 0) {
      const movieId = +this.route.snapshot.paramMap.get('movieId');
      this.apiWrapper.getMovie(movieId).subscribe(m => this.recording.movie = m);
      const argDate = this.route.snapshot.paramMap.get('startTime');
      if(+argDate===0) {
        const date = new Date();
        this.recording.recordedAtTime = date;
      } else {
        const date = new Date(argDate);
        this.recording.recordedAtTime = date;
      }
     
    }
  }

  public save(): void {
    this.apiWrapper.createRecording(this.recording);
    this.router.navigateByUrl('/recordings');
  }

 get year(): number {
  return this.recording.recordedAtTime.getFullYear();
 }
 set year(newYear: number)  {
   this.recording.recordedAtTime.setFullYear(newYear);
 }

 get month(): number {
  return this.recording.recordedAtTime.getMonth() + 1;
 }
 set month(newMonth: number)  {
   this.recording.recordedAtTime.setMonth(newMonth - 1);
 }

 get day(): number {
  return this.recording.recordedAtTime.getDate();
 }
 set day(newDay: number)  {
   this.recording.recordedAtTime.setDate(newDay);
 }

}
