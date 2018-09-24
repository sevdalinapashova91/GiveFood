import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToasterService } from 'angular2-toaster';
import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { HttpParams } from "@angular/common/http";

const mainUrl = 'http://localhost:32794/api/';

@Injectable({
  providedIn: 'root',
})

export class HttpService {
  private httpOptions: any;
    constructor(
    private http: HttpClient,
    private toasterService: ToasterService,
    private oauthService: OAuthService
    ) {
      this.httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
        'Access-Control-Allow-Origin': '*'
      })};
     }

    public get<T> (url: string): Observable<any> {
      return this.http.get<T>(mainUrl + url, this.httpOptions)
       .pipe(catchError(this.handleError([])));
    }

    // public getById<T>(url: string, paramName: string, paramValue: any): Observable<any> {
    //     const headers =  new HttpHeaders({
    //     'Content-Type':  'application/json',
    //     'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
    //     'Access-Control-Allow-Origin': '*',
    //     });
    //     const params = new HttpParams().set('email', paramValue);
    //    return this.http.get<T>(mainUrl + url, { headers: headers, params: params })
    //    .pipe(catchError(this.handleError([])));
    // }

    public post(url: string, data): Observable<any> {
      return this.http.post(mainUrl + url, data, {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
        'Authorization': 'Bearer ' + this.oauthService.getAccessToken(),
      })})
        .pipe(catchError(this.handleError([])));
    }

    public postWithoutHeader(url: string, data): Observable<any> {
      return this.http.post(mainUrl + url, data, {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })})
        .pipe(catchError(this.handleError([])));
    }

    private showError() {
      this.toasterService.pop('error', '', 'Възникна грешка. Моля обърнете се към своя администратор.');
    }

    private handleError<T> ( result?: T) {
    return (error: any): Observable<T> => {
          console.error(error);
          this.showError();
          return of(result as T);
    };
  }
 }
