import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RecordingsComponent } from './recordings/recordings.component';
import { RecordingDetailsComponent } from './recording-details/recording-details.component';
import { MoviesComponent } from './movies/movies.component';


const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'recordings/:id/:movieId/:startTime', component: RecordingDetailsComponent},
  { path: 'recordings', component: RecordingsComponent},
  { path: 'movies', component: MoviesComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
