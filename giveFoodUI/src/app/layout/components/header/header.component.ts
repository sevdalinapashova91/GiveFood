import { NotificationModel, NotificationCountModel } from './../../../shared/services/notification.model';
import { NotificationService } from './../../../shared/services/notification.service';
import { LoginRoutingModule } from './../../../login/login-routing.module';
import { AuthService } from './../../../shared/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
     private _hubConnection: HubConnection | undefined;
    message = '';
    messages: string[] = [];
    pushRightClass = 'push-right';
    public userName = '';
    count: number;
    notifications: NotificationModel[];
    constructor(private translate: TranslateService,
    public router: Router,
    private authService: AuthService,
    private notificationService: NotificationService) {

        this.translate.addLangs(['en', 'fr', 'ur', 'es', 'it', 'fa', 'de', 'zh-CHS']);
        this.translate.setDefaultLang('en');
        const browserLang = this.translate.getBrowserLang();
        this.translate.use(browserLang.match(/en|fr|ur|es|it|fa|de|zh-CHS/) ? browserLang : 'en');

        this.router.events.subscribe(val => {
            if (
                val instanceof NavigationEnd &&
                window.innerWidth <= 992 &&
                this.isToggled()
            ) {
                this.toggleSidebar();
            }
        });
    }

    ngOnInit() {
        this.userName = this.authService.getName();
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('http://localhost:32794/notify')
            .configureLogging(signalR.LogLevel.Information)
            .build();

        this._hubConnection.start().catch(err => console.error(err.toString()));

        this._hubConnection.on('ReceiveMessage', (data: NotificationCountModel) => {
            if (data.userId === this.authService.getUserId()) {
                this.count = data.notificationCount;
            }
        });

        this.notificationService.getUnread().subscribe(data => {
            this.notifications = data,
            this.count = this.notifications.length;
        });
    }
    onLoadNotifcations() {
        this.notificationService.getUnread().subscribe(data => {
            this.notifications = data,
            this.count = this.notifications.length;
        });
    }
    onElementClick(notification: NotificationModel) {
        if (notification.messageType === 4 || notification.messageType === 5) {
        this.notificationService.updateRead(notification.id).subscribe(x => x);
        }
    }
    getText(notification: NotificationModel) {
      return this.notificationService.getNotificationText(notification);
    }

    onApprove(notification): void {
        this.notificationService.approve(notification.id).subscribe(x => x);
        this.notificationService.updateRead(notification.id).subscribe(x => x);
    }
    onReject(notification): void {
        this.notificationService.reject(notification.id).subscribe(x => x);
        this.notificationService.updateRead(notification.id).subscribe(x => x);
    }

    isToggled(): boolean {
        const dom: Element = document.querySelector('body');
        return dom.classList.contains(this.pushRightClass);
    }

    toggleSidebar() {
        const dom: any = document.querySelector('body');
        dom.classList.toggle(this.pushRightClass);
    }

    rltAndLtr() {
        const dom: any = document.querySelector('body');
        dom.classList.toggle('rtl');
    }

    onLoggedout() {
        this.authService.logout();
    }
    onGoToProfile() {
        this.router.navigateByUrl('/profile');
    }
    changeLang(language: string) {
        this.translate.use(language);
    }
}
