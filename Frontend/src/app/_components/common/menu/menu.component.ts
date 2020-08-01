import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { MenuItems } from './menu-items';
import { AuthService } from 'src/app/_services/auth.service';
declare var $: any;

@Component({
	selector: 'app-menu',
	templateUrl: './Menu.component.html',
	styleUrls: ['./Menu.component.scss'],
	encapsulation: ViewEncapsulation.None
})
export class MenuComponent implements OnInit {

	isLoggedIn: any
	menuItems: any;
	selectedMenu: any = null;
	selectedSubMenu: any = null;
	constructor(public menuItemsService: MenuItems, private router: Router, private authService: AuthService) {
		this.router.events.subscribe((ev) => {
			if (ev instanceof NavigationEnd) {
				$('#navbar_global').removeClass('show');
			}
		});
	}

	ngOnInit() {
		this.isLoggedIn = localStorage.getItem('filmunity-userId') != null;
		this.menuItems = this.menuItemsService.getAll();
	}

	menuClick(value) {
		if (this.selectedMenu == value) {
			this.selectedMenu = null;
		}
		else {
			this.selectedMenu = value;
		}
	}

	subMenuClick(value) {
		if (this.selectedSubMenu == value) {
			this.selectedSubMenu = null;
		}
		else {
			this.selectedSubMenu = value;
		}
	}

	logout() {
		this.authService.logout();
		this.router.navigate(['/auth/login']);
	}

	hasRole(allowedRoles: string[]) {
		return this.authService.userHasRole(allowedRoles);
	}

}
