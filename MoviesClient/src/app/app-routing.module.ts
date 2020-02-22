import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RecordingsComponent } from './recordings/recordings.component';
import { RecordingDetailsComponent } from './recording-details/recording-details.component';


const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'recordings/:id/:movieId/:startTime', component: RecordingDetailsComponent},
  { path: 'recordings', component: RecordingsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
