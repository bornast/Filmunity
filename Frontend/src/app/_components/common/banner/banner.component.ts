
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

	/** Title for baner **/
	@Input('title') Title: any = 'Dummy Title';

	/** Description for baner **/
	@Input('desc') Desc: any = 'Description';

	/** Background for baner **/
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
		this.router.navigate(['/listing/list/with-sidebar'], { queryParams: { searchTxt: this.searchTxt, filmType: this.selectedCategoryOption } });
	}
}
