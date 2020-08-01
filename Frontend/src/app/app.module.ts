import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient} from '@angular/common/http';
import { DropzoneModule } from 'ngx-dropzone-wrapper';
import { DROPZONE_CONFIG } from 'ngx-dropzone-wrapper';
import { DropzoneConfigInterface } from 'ngx-dropzone-wrapper';

import { AppComponent } from './app.component';
import { AppRoutes } from './app.routing';



import { ToastrModule } from 'ngx-toastr';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { JwtModule } from '@auth0/angular-jwt';
import { AdminHeaderComponent } from './_components/admin/header/header.component';
import { AdminSidebarComponent } from './_components/admin/sidebar/sidebar.component';
import { AdminMenuItems } from './_components/admin/header/menu-items';
import { AdminPanelLayoutComponent } from './_components/layouts/admin-panel/admin-panel-layout.component';
import { FrontendPanelLayoutComponent } from './_components/layouts/frontend-panel/frontend-panel.component';
import { HeaderComponent } from './_components/header/header.component';
import { FooterComponent } from './_components/footer/footer.component';
import { MenuComponent } from './_components/menu/menu.component';
import { MenuItems } from './_components/menu/menu-items';
import { AuthLayoutComponent } from './_components/layouts/auth/auth-layout.component';

export function tokenGetter() {
	return localStorage.getItem('filmunity-token');
}

const DEFAULT_DROPZONE_CONFIG: DropzoneConfigInterface = {
   // Change this to your upload POST address:
    url: 'https://httpbin.org/post',
    maxFilesize: 50,
    acceptedFiles: 'image/*'
  };

@NgModule({
  declarations: [
      AppComponent,
      AdminPanelLayoutComponent,
      FrontendPanelLayoutComponent,
      AuthLayoutComponent,
      HeaderComponent,
      FooterComponent,
      MenuComponent,
      AdminHeaderComponent,
      AdminSidebarComponent
  ],
  imports: [
	  BrowserModule,
	  FormsModule,
	  BrowserAnimationsModule,
	  ToastrModule.forRoot({
		timeOut: 8000,
		positionClass: 'toast-top-right'
	  }),
      DropzoneModule,
      RouterModule.forRoot(AppRoutes, {scrollPositionRestoration: 'enabled'}),
	  HttpClientModule,
	  JwtModule.forRoot({
		config: {
			tokenGetter: tokenGetter,
			whitelistedDomains: ['localhost:5000'], // to what endpoits do we send authorization headers
			blacklistedRoutes: ['localhost:5000/api/auth'] // to what endpoints do we not send authorization headers
		}
	})
  ],
  providers: [
      MenuItems, 
      AdminMenuItems,
      {
        provide: DROPZONE_CONFIG,
        useValue: DEFAULT_DROPZONE_CONFIG
	  },
	  ErrorInterceptorProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
