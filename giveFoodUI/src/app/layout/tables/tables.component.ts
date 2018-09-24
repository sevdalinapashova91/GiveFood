import { authConfig } from './../../shared/guard/auth.config';
import { AuthService } from './../../shared/services/auth.service';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserModel } from './../../shared/services/user.model';
import { UserService } from './../../shared/services/user.service';
import { ProfileService } from './../../shared/services/profile.service';
import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../router.animations';

@Component({
    selector: 'app-tables',
    templateUrl: './tables.component.html',
    styleUrls: ['./tables.component.scss'],
    animations: [routerTransition()]
})
export class TablesComponent implements OnInit {
    tableTitle: string;
    users: Array<UserModel>;

    constructor(private profileService: ProfileService,
    private userService: UserService,
    private router: Router,
    private authService: AuthService) {
    }

    onElementClick(user) {
        if (this.authService.isAdmin()) {
            this.router.navigate(['/user', user.email]);
        return;
       }
    }
    isAdmin(): boolean {
        return this.authService.isAdmin();
    }
    isGiver(): boolean {
        return this.authService.isGiver();
    }
    isTaker(): boolean {
        return this.authService.isTaker();
    }
    ngOnInit() {
        this.profileService.get()
            .subscribe((data: UserModel ) => {
                 this.tableTitle = data.type === 2 ? 'Дарители на храна' : 'Получатели на храна';
                 this.userService.getUsers(data.type)
                    .subscribe((users: UserModel[]) => {
                        this.users = users;
                 });
        });
    }

    onRequestApprovalToGive(user): void {
        this.userService.requestApprovalToGive(user).subscribe(x => x);
    }

    onRequestApprovalToTake(user): void {
        this.userService.requestApprovalToTake(user).subscribe(x => x);
    }

    getStatus(user: UserModel) {
        if (user.status === 0 || !user.status) {
            return 'Чака одобрение';
        }
        if (user.status === 1) {
            return 'Одобрен';
        }
        if (user.status === 2) {
            return 'Отхвърлен';
        }
    }
}
