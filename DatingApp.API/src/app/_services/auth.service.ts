import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
//Service is not a component hence the Injectable attribute needs to be added
@Injectable()
export class AuthService {

  baseUrl = 'http://localhost:5000/api/auth/';

  constructor(private http: HttpClient) { }

  login(model: any) {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post(this.baseUrl + 'login', model, httpOptions)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
          }
        }));
  }
}
