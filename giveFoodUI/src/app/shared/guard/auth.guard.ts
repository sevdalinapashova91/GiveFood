import { AuthService } from './../services/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private oauthService: OAuthService, private router: Router) { }

    canActivate() {
        if (this.oauthService.hasValidAccessToken()) {
            return true;
        }

        this.router.navigate(['/login']);
        return false;
    }
}

@Injectable()
export class AdminGuard implements CanActivate {

    constructor(private oauthService: OAuthService, private router: Router, private authService: AuthService) { }

    canActivate() {
        if (this.oauthService.hasValidAccessToken()) {
            return this.authService.isAdmin();
        }

        this.router.navigate(['/login']);
        return false;
    }
}

