import { Component, OnInit, Input, Output, AfterViewInit, ViewEncapsulation } from '@angular/core';
declare var $ : any;

@Component({
  selector: 'gallery-slider',
  templateUrl: './gallery-slider.component.html',
  styleUrls: ['./gallery-slider.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class GallerySliderComponent implements OnInit {

   @Input('title') title: any;

   @Input('data') data: any;

   slideConfig =   
      {
         centerMode: true,
         centerPadding: '20%',
         slidesToShow: 2,
         responsive: [
            {
              breakpoint: 1367,
              settings: {
                centerPadding: '15%'
              }
            },
            {
              breakpoint: 1025,
              settings: {
                centerPadding: '0'
              }
            },
            {
              breakpoint: 767,
              settings: {
                centerPadding: '0',
                slidesToShow: 1
              }
            }
         ]
      };

   constructor(){}

   ngOnInit(){}
}
