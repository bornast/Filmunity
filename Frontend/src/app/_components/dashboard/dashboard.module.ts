import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { HomeComponent } from '../home/home.component';
import { DashboardRoutes } from './dashboard.routing';
import { GlobalModule } from '../globalFrontendComponents/global.module';

@NgModule({
  imports: [
    CommonModule,
    GlobalModule,
    RouterModule.forChild(DashboardRoutes),
  ],
  declarations: [ 
     HomeComponent,
   ]
})

export class DashboardModule {}
