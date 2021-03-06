import { Component, OnInit, Input, Output, AfterViewInit, ViewEncapsulation } from '@angular/core';

@Component({
	selector: 'popular-films',
	templateUrl: './popular-films.component.html',
	styleUrls: ['./popular-films.component.css'],
	encapsulation: ViewEncapsulation.None
})
export class PopularFilmsComponent implements OnInit {

	@Input('title') title: any;
	@Input('desc') desc: any;
	@Input('data') data: any;

	slideConfig = {
		centerMode: true,
		centerPadding: '15%',
		slidesToShow: 3,
		dots: true,
		arrows: false,
		responsive: [
			{
				breakpoint: 1441,
				settings: {
					centerPadding: '10%',
					slidesToShow: 3
				}
			},
			{
				breakpoint: 1025,
				settings: {
					centerPadding: '10px',
					slidesToShow: 2,
				}
			},
			{
				breakpoint: 767,
				settings: {
					centerPadding: '10px',
					slidesToShow: 1
				}
			}
		]
	};

	constructor() { }

	ngOnInit() {

	}

	ngAfterViewInit() { }
}
