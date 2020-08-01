import { Injectable } from '@angular/core';

export interface Menu {
	state: string;
	name: string;
	type?: string;
	children?: Menu[];
}

const MENUITEMS = [
	{
		state: '',
		name: 'Home',
		type: 'sub',
		children: [
			{ state: 'home', name: 'Home', type: 'link' },
		]
	},
	{
		state: '',
		name: 'Listing',
		type: 'sub',
		children: [
			{ state: 'film-list', name: 'Films', type: 'link' },
			{ state: 'watchlist-list', name: 'Watchlists', type: 'link' },
			{ state: 'user-list', name: 'Users', type: 'link' }
		]
	},
	{
		state: 'admin',
		name: 'User Panel',
		type: 'sub',
		children: [
			{ state: 'film-list', name: 'Films', type: 'link' }
		]
	},
	{
		state: 'auth',
		name: 'Auth',
		type: 'sub',
		children: [
			{ state: 'login', name: 'Login', type: 'link' },
			{ state: 'signup', name: 'Register', type: 'link' }
		]
	}
];


@Injectable()
export class MenuItems {
	getAll() {		
		let menuItems = JSON.parse(JSON.stringify(MENUITEMS));

		let username = localStorage.getItem('filmunity-username');
		if (username) {
			let profileMenu = {
				state: '',
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
