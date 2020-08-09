import { Component, OnInit, AfterViewInit, ViewEncapsulation } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
declare var $ : any;

@Component({
  selector: 'app-admin-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AdminSidebarComponent implements OnInit {

	
   constructor(private authService: AuthService){}

   ngOnInit(){}

   hasRole(allowedRoles: string[]) {
	   return this.authService.userHasRole(allowedRoles);
   }

   toggleMenu()
   {
      if ( $('app-admin-panel').hasClass( "sidebar-in" ) ) {
         $('app-admin-panel').removeClass("sidebar-in");
      }
      else
      {
         $('app-admin-panel').addClass("sidebar-in");
      }
      // this.sidebarIn = !this.sidebarIn;
   }
}
