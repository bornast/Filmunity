import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { DashboardOneComponent } from './DashboardOne/DashboardOne.component';
import { DashboardRoutes } from './dashboard.routing';
import { GlobalModule } from '../globalFrontendComponents/global.module';

@NgModule({
  imports: [
    CommonModule,
    GlobalModule,
    RouterModule.forChild(DashboardRoutes),
  ],
  declarations: [ 
     DashboardOneComponent,
   ]
})

export class DashboardModule {}
