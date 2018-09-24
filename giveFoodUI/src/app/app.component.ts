import { AuthService } from './shared/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { authConfig } from './shared';
import { OAuthService, JwksValidationHandler} from 'angular-oauth2-oidc';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
    constructor(
        private oauthService: OAuthService,
        private authService: AuthService) {
        this.oauthService.configure(authConfig);
        this.oauthService.tokenValidationHandler = new JwksValidationHandler();
        this.oauthService.loadDiscoveryDocumentAndTryLogin();
    }

    public sendMessage(): void {
        const data = `Sent: ${this.message}`;

        if (this._hubConnection) {
            this._hubConnection.invoke('SendNotification', data);
        }

        this.messages.push(data);
    }

    ngOnInit() {
    }

}
