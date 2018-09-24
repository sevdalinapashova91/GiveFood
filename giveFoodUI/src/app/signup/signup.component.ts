import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { SingUpModel } from './signup.model';
import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../router.animations';
import { AuthService } from '../shared/services';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.scss'],
    animations: [routerTransition()]
})
export class SignupComponent implements OnInit {
    registerFormGroup: FormGroup;
    model: SingUpModel;
    constructor(private formBuilder: FormBuilder, private authService: AuthService, private router: Router) {
        this.model =  new SingUpModel('', '', '', '', '1');
        this.registerFormGroup = this.formBuilder.group({
            name: [this.model.name, Validators.required],
            email: [this.model.email, [ Validators.required, Validators.email]],
            password: [this.model.password, [Validators.required,
                Validators.minLength(8),
                Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)]],
            confirmPassword: [ this.model.confirmPassword, Validators.required],
            type: [this.model.type, Validators.required]
        },{ validator: this.checkIfMatchingPasswords('password', 'confirmPassword')});
    }

    checkIfMatchingPasswords(passwordKey: string, passwordConfirmationKey: string) {
          return (group: FormGroup) => {
            const passwordInput = group.controls[passwordKey],
                passwordConfirmationInput = group.controls[passwordConfirmationKey];
            if (passwordInput.value !== passwordConfirmationInput.value) {
              return passwordConfirmationInput.setErrors({notEquivalent: true})
            } else {
                return passwordConfirmationInput.setErrors(null);
            }
          };
        }

    get registerForm() { return this.registerFormGroup.controls; }

    onSubmit() {
        this.authService.register(this.registerFormGroup.value)
        .subscribe(data => {
            this.router.navigateByUrl('/login');
        });
    }
    ngOnInit() {
    }
}
