import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpErrorResponse, HTTP_INTERCEPTORS, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { catchError, switchMap, filter, take, finalize } from 'rxjs/operators';
import { throwError, Observable, BehaviorSubject, of } from 'rxjs';
import { ToastService } from "../_services/toast.service";
import { AuthService } from "../_services/auth.service";
import { Router } from "@angular/router";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

	private AUTH_HEADER = "Authorization";
	private refreshTokenInProgress = false;
	private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

	constructor(private toast: ToastService, private authService: AuthService, private router: Router) { }

	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

		return next.handle(req).pipe(
			catchError((error: HttpErrorResponse) => {

				if (error.status === 400 && req.url.includes("refreshToken")) {
					this.router.navigate(['/session/login']);
					return throwError(error);
				}				

				if (error && error.status === 401 && !req.url.includes("login")) {
					// 401 errors are most likely going to be because we have an expired token that we need to refresh.
					if (this.refreshTokenInProgress) {
						// If refreshTokenInProgress is true, we will wait until refreshTokenSubject has a non-null value
						// which means the new token is ready and we can retry the request again
						return this.refreshTokenSubject.pipe(
							filter(result => result !== null),
							take(1),
							switchMap(() => next.handle(this.addAuthenticationToken(req)))
						);
					} else {
						this.refreshTokenInProgress = true;

						// Set the refreshTokenSubject to null so that subsequent API calls will wait until the new token has been retrieved
						this.refreshTokenSubject.next(null);

						return this.authService.refreshToken().pipe(
							switchMap(() => {
								this.refreshTokenSubject.next(true);
								return next.handle(this.addAuthenticationToken(req));
							}),
							catchError(error => {
								this.handleErrors(error);
								return of(error);
							}),
							// When the call to refreshToken completes we reset the refreshTokenInProgress to false
							// for the next time the token needs to be refreshed
							finalize(() =>  {
								this.refreshTokenInProgress = false;
							})
						);
					}
				} else {					
					this.handleErrors(error);
					return throwError(error);
				}
			})
		) as Observable<HttpEvent<any>>;
	}

	private addAuthenticationToken(request: HttpRequest<any>): HttpRequest<any> {
		// If we do not have a token yet then we should not set the header.
		let token = localStorage.getItem("filmunity-token");

		if (!token)
			return request;

		return request.clone({headers: request.headers.set(this.AUTH_HEADER, "Bearer " + token)});
	}

	handleErrors(error: HttpErrorResponse) {

		if (error.status === 0) {
			this.toast.error("Server is not responding!");
		}
		else if (error.status === 500) {
			this.toast.error("Server error!");
		}
		else if (error.status === 403) {
			this.toast.error("Forbidden!");
		}

		else if (error.status === 401) {
			this.toast.error("Unauthorized!");
			this.router.navigate(['/session/login']);
		}

		else if (error.status === 404) {
		}

		else if (error instanceof HttpErrorResponse) {
			this.handleErrorMessages(error.error);
		}
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