import { NotificationsRoutingModule } from './notifications-routing.module';
import { NotificationsComponent } from './notifications.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


@NgModule({
  imports: [
    CommonModule,
    NotificationsRoutingModule,
  ],
  declarations: [ NotificationsComponent ],
})
export class NotificationsModule { }
