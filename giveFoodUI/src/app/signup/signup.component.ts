import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../router.animations';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.scss'],
    animations: [routerTransition()]
})
export class SignupComponent implements OnInit {
    constructor() {}
    registerFormName : string;
    
    setSignUpForm(formName : string){
        this.registerFormName = formName
    }

    ngOnInit() {}
}
