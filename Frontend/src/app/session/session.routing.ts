import { Routes } from '@angular/router';

import { LoginComponent } from './Login/Login.component';
import { SignUpComponent } from './SignUp/SignUp.component';

export const SessionRoutes: Routes = [
	{
		path: '',
		redirectTo: 'login',
		pathMatch: 'full',
	},
	{
		path: '',
		children: [{
			path: 'login',
			component: LoginComponent
		}, {
			path: 'signup',
			component: SignUpComponent
		}]
	}
];
