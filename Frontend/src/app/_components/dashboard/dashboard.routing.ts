import { Routes } from '@angular/router';

import { HomeComponent } from '../home/home.component';

export const DashboardRoutes: Routes = [
{
  path: '',
  component: HomeComponent
},
{
  path: 'version1',
  component: HomeComponent
}];
