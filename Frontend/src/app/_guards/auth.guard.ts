import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { ToastService } from '../_services/toast.service';
import {Location} from '@angular/common';

@Injectable({
	providedIn: 'root'
})
export class AuthGuard implements CanActivate {
	constructor(private authService: AuthService, private router: Router, private toast: ToastService, private _location: Location) {}

	canActivate(next: ActivatedRouteSnapshot): boolean {
		/* 
			get roles listed in data property of route listed in routes.ts
			since our auth guard is protecting child routes we need to say
			next.firstChild.data['roles], if we werent protecting 
			child routes it would be: next.data['roles]
		*/
		
		let roles: string[];
		
		if (next.firstChild != null)
			roles = next.firstChild.data['roles'] as Array<string>;
		else
			roles = next.data['roles'] as Array<string>;
			
		if (roles) {
			const match = this.authService.userHasRole(roles);
			if (match) {
				return true;
			} else {
				return this.handleUnauthorizedAccess();
			}
		}
		if (this.authService.loggedIn()) {
			return true;
		}
		else {
			return this.handleUnauthorizedAccess();
		}
	}

	private handleUnauthorizedAccess(): boolean {
		this.toast.error('You are not authorized to access this area');
		this.router.navigate(['home']);
		return false;
	}

}
