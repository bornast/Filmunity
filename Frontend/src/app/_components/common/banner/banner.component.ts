
import { FILMTYPE } from 'src/app/_constants/filmTypeConst';
import { ViewEncapsulation, Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
	selector: 'banner',
	templateUrl: './Banner.component.html',
	styleUrls: ['./Banner.component.css'],
	encapsulation: ViewEncapsulation.None
})
export class BannerComponent implements OnInit {

	searchTxt: string;
	categoryOptions: any[];
	selectedCategoryOption: string;

	@Input('title') title: any;
	@Input('desc') desc: any;
	@Input('bgImageUrl') bgImageUrl: any;

	constructor(private router: Router) { }

	ngOnInit() {
		this.loadCategoryOptions();
	}

	loadCategoryOptions() {
		this.categoryOptions = [
			{
				id: null, name: "All Films"
			},
			{
				id: FILMTYPE.movie, name: "Movies"
			},
			{
				id: FILMTYPE.tvShow, name: "Tv Shows"
			}
		];

		this.selectedCategoryOption = null;
	}

	search() {
		this.router.navigate(['/film-list'], { queryParams: { searchTxt: this.searchTxt, filmType: this.selectedCategoryOption } });
	}
}
