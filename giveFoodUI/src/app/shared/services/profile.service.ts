import { UserModel } from './user.model';
import { HttpService } from './http.service';
import { Observable, of } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
    private profileGetUrl = 'profile/get';
    private profileUpdateUrl = 'profile/UpdateUserProfile';
    private uploadUrl = 'upload/upload';

    constructor(private http: HttpService) { }

    public get(): Observable<any> {
        return this.http.get<ProfileModel>(this.profileGetUrl);
    }

    public update(data: any): Observable<any> {
        return this.http.post(this.profileUpdateUrl, { name: data.name, description: data.description });
    }

    public uploadFile(data: any): Observable<any> {
        return this.http.post(this.uploadUrl, data);
    }
 }
