import { Component, OnInit, Input, Output, AfterViewInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'recent-watchlists',
  templateUrl: './recent-watchlists.component.html',
  styleUrls: ['./recent-watchlists.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class RecentWatchlistsComponent implements OnInit {

   @Input('title') title: any = 'Dummy Title';

   @Input('data') data: any;

   constructor(){}

   ngOnInit(){}

   ngAfterViewInit()
   {

   }
}
