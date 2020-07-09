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

	private storeUserInfoToLocalStorage(tokenObject: any) {
		this.decodedToken = this.jwtHelper.decodeToken(tokenObject.token);
		localStorage.setItem('filmunity-token', tokenObject.token);
		localStorage.setItem('filmunity-refreshToken', tokenObject.refreshToken);
		localStorage.setItem('filmunity-tokenObject', this.decodedToken.username);
	}

}
