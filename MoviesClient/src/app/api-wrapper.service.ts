import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiWrapperService {

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    
  }
}
