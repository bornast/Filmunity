import { Injectable } from '@angular/core';
import { FILMTYPE } from 'src/app/_constants/filmTypeConst';

export interface Menu {
	state: string;
	name: string;
	type?: string;
	children?: Menu[];
}

const MENUITEMS = [
	{ state: '/home', name: 'Home', type: 'link' },
	{ state: '/movie-list', name: 'Movies', type: 'link'},
	{ state: '/tvshow-list', name: 'Tv Shows', type: 'link'},
	{ state: '/watchlist-list', name: 'Watchlists', type: 'link' },
	{ state: '/user-list', name: 'Users', type: 'link' },
	{ state: '/admin/film-list', name: 'Admin Panel', type: 'link', allowedRoles: ['Admin', 'Moderator']},
	{ state: '/auth/login', name: 'Login', type: 'link', isLoggedIn: false, },
	{ state: '/auth/register', name: 'Register', type: 'link', isLoggedIn: false, }
];


@Injectable()
export class MenuItems {
	getAll() {		
		let menuItems = JSON.parse(JSON.stringify(MENUITEMS));

		let username = localStorage.getItem('filmunity-username');
		if (username) {
			let profileMenu = {
				state: '/',
				name: localStorage.getItem('filmunity-username'),
				type: 'sub',
				children: [
					{ state: 'my-profile', name: 'My profile', type: 'link' },
					{ state: 'user-watchlists', name: 'My Watchlists', type: 'link' },
					{ state: 'friendship-request-list', name: 'My Friendship Requests', type: 'link' },
					{ state: 'friend-list', name: 'My Friends', type: 'link' },
					{ state: 'logout', name: 'Logout', type: 'link' },
				]
			};			
			menuItems.push(profileMenu);
		}
		
		return menuItems;
	}
}
