import { AuthService } from './../../shared/services/auth.service';
import { NotificationService } from './../../shared/services/notification.service';
import { NotificationModel } from './../../shared/services/notification.model';
import { OnInit, Component } from '@angular/core';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.scss']
})
export class NotificationsComponent implements OnInit {
    userName: string;
    notifications: NotificationModel[];
    constructor(private notificationService: NotificationService, private authService: AuthService) {
        this.userName = authService.getName();
    }

    ngOnInit(): void {
        this.notificationService.getAll().subscribe(data => this.notifications = data);
    }

    getNotificationText(notification): string {
        return this.notificationService.getNotificationText(notification);
    }
    onApprove(notification: NotificationModel): void {
        notification.isRead = true;
        this.notificationService.approve(notification.id).subscribe(x => x);
        this.notificationService.updateRead(notification.id).subscribe(x => x);
    }
    onReject(notification: NotificationModel): void {
        notification.isRead = true;
        this.notificationService.reject(notification.id).subscribe(x => x);
        this.notificationService.updateRead(notification.id).subscribe(x => x);
    }


}
