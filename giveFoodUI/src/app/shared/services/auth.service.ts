import { HttpService } from './http.service';

export class AuthService {
    private loginUrl : string = '/api/auth/login';
    private registerUrl : string = '/api/auth/register';

    constructor(private http: HttpService) { }
    
    public login(){

    };
    
    public register(){

    } 
    
    public logout(){
        
    }
 }