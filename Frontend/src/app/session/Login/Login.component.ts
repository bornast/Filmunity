import { Component, OnInit, AfterViewInit, ViewEncapsulation } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { ToastrService } from 'ngx-toastr';
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

	ngOnInit() { }

	ngAfterViewInit() {

	}

	login() {
		this.authService.login(this.loginObject).subscribe(() => {
			this.toast.success('Logged in successfully');
			this.router.navigate(['/home']);
		});
	}

}
