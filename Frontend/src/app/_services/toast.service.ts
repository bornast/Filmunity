import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
	providedIn: 'root'
})
export class ToastService {

	constructor(private toastr: ToastrService) { }

	success(msg: any, title: string = 'Info') {
		this.toastr.success(msg, title);
	}

	error(msg: any, title: string = 'Error') {
		this.toastr.error(msg, title);
	}

}
