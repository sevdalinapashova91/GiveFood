import { UserModel } from './user.model';
import { HttpService } from './http.service';
import { Observable, of } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UserService {
    private getTakersUrl = 'approval/getTakers';
    private getGiversUrl = 'approval/getGivers';
    private getAllUsersUrl = 'admin/getall';
    private getUserUrl = 'admin/get/?email=';
    private evaluteUserUrl = 'admin/EvaluateUser';
    private requestApprovalToGiveUrl = 'approval/RequestApprovalToGive';
    private requestApprovalToTakeUrl = 'approval/RequestApprovalToTake';

    constructor(private http: HttpService) { }

    public getUser(email: string): Observable<UserModel> {
        return this.http.get<UserModel>(this.getUserUrl + email);
    }
    public evaluteUser(data: any): Observable<any> {
        return this.http.post(this.evaluteUserUrl, data);
    }
    public getUsers(type: number): Observable<UserModel[]> {
        switch (type) {
            case 0: return this.getAllUsers();
            case 1: return this.getTakers();
            case 2: return this.getGivers();
        }
    }
    public requestApprovalToGive(data: any): Observable<any> {
        return this.http.post(this.requestApprovalToGiveUrl, data);
    }
    public requestApprovalToTake(data: any): Observable<any> {
        return this.http.post(this.requestApprovalToTakeUrl, data);
    }
    private getTakers(): Observable<UserModel[]> {
        return this.http.get<UserModel[]>(this.getTakersUrl);
    }

    private getGivers(): Observable<UserModel[]> {
        return this.http.get<UserModel[]>(this.getGiversUrl);
    }

    private getAllUsers(): Observable<UserModel[]> {
        return this.http.get<UserModel[]>(this.getAllUsersUrl);
    }

 }
