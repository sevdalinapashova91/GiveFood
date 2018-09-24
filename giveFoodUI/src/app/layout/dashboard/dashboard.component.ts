import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../router.animations';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss'],
    animations: [routerTransition()]
})
export class DashboardComponent implements OnInit {
    public alerts: Array<any> = [];
    public sliders: Array<any> = [];

    constructor() {
        this.sliders.push(
            {
                imagePath: 'assets/images/givefood1.jpg',
                label: 'Дари храна днес!',
                text: 'Стани част от дарителите на храна! Помогни на нуждаещ се!'
            },
            {
                imagePath: 'assets/images/givefood2.jpg',
                label: 'Получи храна днес!',
                text: 'Нашите дарители чакат да ти дарят храна!'
            },
            {
                imagePath: 'assets/images/givefood3.jpg',
                label: 'Бъди част от промяната!',
                text: 'Заедно можем да направим света по-добро място.'
            }
        );
    }

    ngOnInit() {}

    public closeAlert(alert: any) {
        const index: number = this.alerts.indexOf(alert);
        this.alerts.splice(index, 1);
    }
}
