import { AuthContext } from './auth-context';
import { UserModel } from './user.model';
import { HttpService } from './http.service';
import {  OAuthService } from 'angular-oauth2-oidc';
import { Observable, of } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
    private registerUrl = 'account/register';
    authContext: AuthContext;

    constructor(private http: HttpService,
        private oauthService: OAuthService) { }

    public login() {
        this.oauthService.initImplicitFlow();
    }

    public register(data: any): Observable<any> {
        return this.http.postWithoutHeader(this.registerUrl, {
            name: data.name,
            email: data.email,
            password: data.password,
            type: +data.type
         });
    }


    public logout() {
        this.oauthService.logOut();
    }

    public loadSercurityContext() {
        this.http.get<AuthContext>('Identity/GetAuthContext').subscribe(context => {
            this.authContext = context;
        }, error => console.error(error));
    }

    public isAdmin(): boolean {
        return this.oauthService.getIdentityClaims()['role'] === 'Admin';
    }
    public getUserId(): string {
        return this.oauthService.getIdentityClaims()['sub'];
    }
    public isGiver(): boolean {
        return this.oauthService.getIdentityClaims()['role'] === 'Giver';
    }

     public isTaker(): boolean {
        return this.oauthService.getIdentityClaims()['role'] === 'Taker';
    }

    public isInRole(): boolean {
        return this.oauthService.getIdentityClaims()['role'] !== undefined;
    }

    public getUserData(): UserModel {
         const claims = this.oauthService.getIdentityClaims();

        if (!claims) {
            return null;
        }

        return new UserModel(claims['name'], claims['email'], claims['description'], claims['type']);
    }

    public getName(): string {
         const claims = this.oauthService.getIdentityClaims();

        if (!claims) {
            return null;
        }
        return claims['name'];
    }
 }
