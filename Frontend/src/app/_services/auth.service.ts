import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable({
	providedIn: 'root'
})
export class AuthService {

	baseUrl = environment.apiUrl + 'auth/';
	jwtHelper = new JwtHelperService();
	decodedToken: any;
	// TODO: add user interface
	currentUser: any;

	constructor(private http: HttpClient) { }

	register(model: any) {
		return this.http.post(this.baseUrl + 'register', model);
	}

	login(model: any) {
		return this.http.post(this.baseUrl + 'login', model)
			.pipe(
				map((response: any) => {
					const user = response;
					if (user) {
						this.storeUserInfoToLocalStorage(user);
						// this.currentUser = user.user;
					}
				})
			);
	}

	loginWithFacebook(model: any) {
		return this.http.post(this.baseUrl + 'loginWithFacebook', model)
			.pipe(
				map((response: any) => {
					const user = response;
					if (user) {
						this.storeUserInfoToLocalStorage(user);
						// this.currentUser = user.user;
					}
				})
			);
	}

	refreshToken() {
		var model = {
			token: localStorage.getItem('filmunity-token'),
			refreshToken: localStorage.getItem('filmunity-refreshToken')
		};

		return this.http.post(this.baseUrl + 'refreshToken', model)
			.pipe(
				map((response: any) => {
					const user = response;
					if (user) {
						this.storeUserInfoToLocalStorage(user);
						// this.currentUser = user.user;
					}
				})
			);
	}	

	loggedIn(): boolean {		
		const token = localStorage.getItem('filmunity-token');
		return token != null;
		// TODO: check if token is expired
		// return !this.jwtHelper.isTokenExpired(token);
	}

	userHasRole(allowedRoles): boolean {
		let isMatch = false;
		if (this.decodedToken == null)
			return isMatch;
		const userRoles = this.decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] as Array<string>;
		console.log("user roles are", userRoles);
		allowedRoles.forEach(element => {
			if (userRoles.includes(element)) {
				isMatch = true;
				return;
			}
		});
		return isMatch;
	}

	private storeUserInfoToLocalStorage(tokenObject: any) {
		this.decodedToken = this.jwtHelper.decodeToken(tokenObject.token);
		localStorage.setItem('filmunity-token', tokenObject.token);
		localStorage.setItem('filmunity-refreshToken', tokenObject.refreshToken);
		localStorage.setItem('filmunity-username', this.decodedToken.username);
	}

}
