import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

	title = 'app';
	jwtHelper = new JwtHelperService();

	constructor(private authService: AuthService) {}

	// update all components with these default values
	ngOnInit() {
		const token = localStorage.getItem('filmunity-token');
		// const user: any = JSON.parse(localStorage.getItem('user'));
		if (token) {
			this.authService.decodedToken = this.jwtHelper.decodeToken(token);
		}
		// if (user) {
		// 	this.authService.currentUser = user;
		// }
	}

}
