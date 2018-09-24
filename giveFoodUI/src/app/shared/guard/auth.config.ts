import {  AuthConfig } from 'angular-oauth2-oidc';

 export const authConfig: AuthConfig = {
     // Url of the Identity Provider
     issuer: 'http://localhost:5000',
     requireHttps: false,

     requestAccessToken: true,

     // URL of the SPA to redirect the user to after login
     redirectUri: 'http://localhost:4200/profile',
     postLogoutRedirectUri: 'http://localhost:4200/login',

     // URL of the SPA to redirect the user after silent refresh
    //  silentRefreshRedirectUri: 'http://localhost:4200/assets/silent-refresh.html',

     // The SPA's id. The SPA is registerd with this id at the auth-server
     clientId: 'angularClient',


     // set the scope for the permissions the client should request
     // The first three are defined by OIDC. The 4th is a usecase-specific one
     scope: 'openid profile email givefoodapi'
};
