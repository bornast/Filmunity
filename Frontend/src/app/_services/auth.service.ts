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

	login(model: any) {
		return this.http.post(this.baseUrl + 'login', model)
			.pipe(
				map((response: any) => {
					const user = response;
					if (user) {
						this.decodedToken = this.jwtHelper.decodeToken(user.token);
						localStorage.setItem('filmunity-token', user.token);
						localStorage.setItem('filmunity-user', this.decodedToken.unique_name);
						// this.currentUser = user.user;
					}
				})
			);
	}

}
