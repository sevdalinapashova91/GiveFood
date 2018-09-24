import { NotificationModel } from './notification.model';
import { UserModel } from './user.model';
import { HttpService } from './http.service';
import { Observable, of } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
    private notificationsGetAllUrl = 'notification/getAll';
    private notificationsGetUnreadUrl = 'notification/getUnread';
    private notificationGetUrl = 'notification/get?id=';
    private notificationDeleteUrl = 'notification/delete';
    private notificationUpdateReadUrl = 'notification/updateread';
    private notificationApprovalUrl = 'approval/approve';
    private notificationRejectUrl = 'approval/rejectapproval';

    constructor(private http: HttpService) { }

    public getAll(): Observable<any> {
        return this.http.get<NotificationModel[]>(this.notificationsGetAllUrl);
    }

    public getUnread(): Observable<any> {
        return this.http.get<NotificationModel[]>(this.notificationsGetUnreadUrl);
    }

    public get(id: number): Observable<any> {
        return this.http.get<NotificationModel>(this.notificationGetUrl + id);
    }

    public delete(id: number): Observable<any> {
        return this.http.post(this.notificationDeleteUrl, {id: id});
    }

     public updateRead(id: number): Observable<any> {
        return this.http.post(this.notificationUpdateReadUrl, {id: id});
    }

    public approve(id: number): Observable<any> {
        return this.http.post(this.notificationApprovalUrl, {id: id});
    }
    public reject(id: number): Observable<any> {
        return this.http.post(this.notificationRejectUrl, {id: id});
    }

    public getNotificationText(notification: NotificationModel): string {
        if (notification.messageType === 2) {
            return `Примемаш ли ${notification.senderName} да ти дари храна?`;
        }
        if (notification.messageType === 3) {
            return `Приемаш ли да дариш храна на ${notification.senderName}?`;
        }
        if (notification.messageType === 4) {
            return `Запитването ти беше отхвърлено от ${notification.senderName}`;
        }
        if (notification.messageType === 5) {
            return `Запитването ти беше одобрено от ${notification.senderName}`;
        }
    }
 }
