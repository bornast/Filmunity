import { Component, OnInit, AfterViewInit, ViewEncapsulation } from '@angular/core';
import { AdminMenuItems } from './admin-menu-items';

@Component({
  selector: 'app-admin-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AdminHeaderComponent implements OnInit {

   constructor(public adminMenuItems: AdminMenuItems){}

   ngOnInit(){}

   ngAfterViewInit()
   {
   }
}
