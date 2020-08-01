import { Component, OnInit, Input, Output, AfterViewInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'recent-watchlists',
  templateUrl: './recent-watchlists.component.html',
  styleUrls: ['./recent-watchlists.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class RecentWatchlistsComponent implements OnInit {

   /** Title for baner **/
   @Input('title') Title: any = 'Dummy Title';

   /** Background for baner **/
   @Input('data') Data: any;

   constructor(){}

   ngOnInit(){}

   ngAfterViewInit()
   {

   }
}
