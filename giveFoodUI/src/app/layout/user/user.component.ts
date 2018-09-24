import { UserModel } from './../../shared/services/user.model';
import { UserService } from './../../shared/services/user.service';
import { OnInit, Component } from '@angular/core';
import { routerTransition } from '../../router.animations';
import { ActivatedRoute } from '@angular/router';
@Component({
    selector: 'app-user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.scss'],
    animations: [routerTransition()]
})
export class UserComponent implements OnInit {
    email: string;
    user: UserModel;

    constructor(private route: ActivatedRoute, private userService: UserService) {
        this.user = new UserModel('', '', '', 0);
    }
    isApproved(): boolean {
        return this.user.status === 1;
    }
    onApproveClick(): void {
        this.user.status = 1;
        this.evaluate(true);
    }

    onRejectClick(): void {
        this.evaluate(false);
    }

    private evaluate(isApproved: boolean) {
        this.userService.evaluteUser(
            {
                email: this.user.email,
                type: this.user.type,
                isApproved: isApproved
            }).subscribe(x => x);
    }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
        this.email = params['id'];
        });

        this.userService.getUser(this.email)
            .subscribe((data: UserModel ) => this.user = data);
    }
}
