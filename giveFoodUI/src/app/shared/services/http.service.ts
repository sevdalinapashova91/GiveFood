import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToasterService } from 'angular2-toaster';
   const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Authorization': 'my-auth-token'
  })}

export class HttpService {
 
    constructor(
    private http: HttpClient,
    private toasterService: ToasterService) { }

    public get<T> (url:string, httpOptions) : Observable<any>
    {
      return this.http.get<T>(url)
       .pipe(catchError(this.handleError([])));
    }

    public post(url : string, data): Observable<object>{
      return this.http.post(url, data, httpOptions)
        .pipe(catchError(this.handleError([])));
    }
    
    private showError() {
      this.toasterService.pop('error','', 'Възникна грешка. Моля обърнете се към своя администратор.');
    }

    private handleError<T> ( result?: T) {
    return (error: any): Observable<T> => { 
          console.error(error); 
          this.showError();
          return of(result as T);
    }};   
 }