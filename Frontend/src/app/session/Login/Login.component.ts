import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { ToastService } from 'src/app/_services/toast.service';
import { Router } from '@angular/router';

@Component({
	selector: 'login',
	templateUrl: './Login.component.html',
	styleUrls: ['./Login.component.scss'],
	encapsulation: ViewEncapsulation.None
})
export class LoginComponent implements OnInit {

	loginObject: any = {};

	constructor(private authService: AuthService, private toast: ToastService, private router: Router) { }

	ngOnInit() { 
		this.fbLibrary();
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
				this.toast.success('Failed to login!');
			}
			
		}, {scope: 'email'});
	}

	fbLibrary() {
 
		(window as any).fbAsyncInit = function() {
		  window['FB'].init({
			appId      : '952054345240758',
			cookie     : true,
			xfbml      : true,
			version    : 'v7.0'
		  });
		  window['FB'].AppEvents.logPageView();
		};
	 
		(function(d, s, id){
		   var js, fjs = d.getElementsByTagName(s)[0];
		   if (d.getElementById(id)) {return;}
		   js = d.createElement(s); js.id = id;
		   js.src = "https://connect.facebook.net/en_US/sdk.js";
		   fjs.parentNode.insertBefore(js, fjs);
		 }(document, 'script', 'facebook-jssdk'));
	 
	}

}
