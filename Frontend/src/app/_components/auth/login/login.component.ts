// import { Component, OnInit, ViewEncapsulation } from 'src/app/_components/session/login/node_modules/@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { ToastService } from 'src/app/_services/toast.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Component, ViewEncapsulation, OnInit } from '@angular/core';


@Component({
	selector: 'login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss'],
	encapsulation: ViewEncapsulation.None
})
export class LoginComponent implements OnInit {

	loginObject: any = {};

	response: any;

	constructor(private authService: AuthService, private toast: ToastService, private router: Router, private route: ActivatedRoute) { }

	ngOnInit() {
		this.fbLibrary();
		this.twitterLogin();
	}

	twitterLogin() {
		this.route.queryParamMap.subscribe(() => {
			const oauth_token = this.route.snapshot.queryParamMap.get('oauth_token');
			const oauth_verifier = this.route.snapshot.queryParamMap.get("oauth_verifier");			
			if (oauth_token && oauth_verifier) {
				let loginObj = {
					OAuthToken: oauth_token, 
					OAuthVerifier: oauth_verifier
				}
				this.authService.loginWithTwitter(loginObj).subscribe(() => {
					this.router.navigate(['/home']);
				});
			}
		});
	}

	login() {
		this.authService.login(this.loginObject).subscribe(() => {
			this.toast.success('Logged in successfully');
			this.router.navigate(['/home']);
		});
	}

	loginFacebook() {

		window['FB'].login((response) => {

			if (response["authResponse"] != null && response["authResponse"]["accessToken"] != null) {

				let loginObject = {
					"accessToken": response["authResponse"]["accessToken"]
				};

				this.authService.loginWithFacebook(loginObject).subscribe(() => {
					this.toast.success('Logged in successfully');
					this.router.navigate(['/home']);
				});

			} else {
				this.toast.error('Failed to login!');
			}

		}, { scope: 'email' });
	}

	loginTwitter() {

		this.authService.getTwitterRequestToken().subscribe((response) => {
			this.response = response;
		},
			() => { },
			() => {
				location.href = "https://api.twitter.com/oauth/authenticate?oauth_token=" + this.response.oauth_token;
			}
		);
	}

	fbLibrary() {

		(window as any).fbAsyncInit = function () {
			window['FB'].init({
				appId: '952054345240758',
				cookie: true,
				xfbml: true,
				version: 'v7.0'
			});
			window['FB'].AppEvents.logPageView();
		};

		(function (d, s, id) {
			var js, fjs = d.getElementsByTagName(s)[0];
			if (d.getElementById(id)) { return; }
			js = d.createElement(s); js.id = id;
			js.src = "https://connect.facebook.net/en_US/sdk.js";
			fjs.parentNode.insertBefore(js, fjs);
		}(document, 'script', 'facebook-jssdk'));

	}

}
