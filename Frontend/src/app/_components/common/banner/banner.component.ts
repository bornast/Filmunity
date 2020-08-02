
import { FILMTYPE } from 'src/app/_constants/filmTypeConst';
import { ViewEncapsulation, Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
	selector: 'banner',
	templateUrl: './Banner.component.html',
	styleUrls: ['./Banner.component.scss'],
	encapsulation: ViewEncapsulation.None
})
export class BannerComponent implements OnInit {

	searchTxt: string;
	categoryOptions: any[];
	selectedCategoryOption: string;

	@Input('title') Title: any;
	@Input('desc') Desc: any;
	@Input('bgImageUrl') BgImageUrl: any = 'assets/images/main-search-background-01.jpg';

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
