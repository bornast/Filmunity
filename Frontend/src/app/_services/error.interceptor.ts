import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { ToastService } from "./toast.service";
@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

	constructor(private toast: ToastService) { }

	intercept(
		req: import("@angular/common/http").HttpRequest<any>, 
		next: import("@angular/common/http").HttpHandler
	): import("rxjs").Observable<import("@angular/common/http").HttpEvent<any>> {
		return next.handle(req).pipe(
			catchError(error => {

				if (error.status === 401) {
					this.toast.error("Unauthorized!");
					return throwError(error.statusText);
				}

				if (error instanceof HttpErrorResponse) {
					this.handleErrorMessages(error.error);
					return throwError(error);
				}
			})
		)
	}

	handleErrorMessages(serverErrors: any) {
		if (serverErrors != null && typeof serverErrors === 'object') {
			for (const key in serverErrors) {
				if (serverErrors[key]) {
					for (const errorMsgKey in serverErrors[key]) {
						this.toast.error(serverErrors[key][errorMsgKey], key);
					}
				}
			}
		}
	}

}

export const ErrorInterceptorProvider = {
	provide: HTTP_INTERCEPTORS,
	useClass: ErrorInterceptor,
	multi: true
}