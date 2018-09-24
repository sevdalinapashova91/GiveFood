import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from './../bs-component/components/modal/modal.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TablesRoutingModule } from './tables-routing.module';
import { TablesComponent } from './tables.component';
import { PageHeaderModule } from './../../shared';

@NgModule({
    imports: [CommonModule, TablesRoutingModule, PageHeaderModule, NgbModule.forRoot()],
    declarations: [TablesComponent, ModalComponent]
})
export class TablesModule {}
