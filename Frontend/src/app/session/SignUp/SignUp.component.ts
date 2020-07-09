import { Component, OnInit, AfterViewInit, ViewEncapsulation } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { ToastService } from 'src/app/_services/toast.service';
import { Router } from '@angular/router';

@Component({
	selector: 'signup',
	templateUrl: './SignUp.component.html',
	styleUrls: ['./SignUp.component.scss'],
	encapsulation: ViewEncapsulation.None
})
export class SignUpComponent implements OnInit {

	registerObject: any = {};

	constructor(private authService: AuthService, private toast: ToastService, private router: Router) { }

	ngOnInit() { }

	register() {

		this.authService.register(this.registerObject).subscribe(() => {
			this.toast.success('Registered successfully');
			this.router.navigate(['/session/login']);
		});
	}
}
