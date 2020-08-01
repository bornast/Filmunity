import { Component, OnInit, AfterViewInit, ViewEncapsulation } from '@angular/core';
import { AdminMenuItems } from './menu-items';

@Component({
  selector: 'app-admin-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AdminHeaderComponent implements OnInit {

   constructor(public adminMenuItems: AdminMenuItems){}

   ngOnInit(){}

   ngAfterViewInit()
   {
   }
}
