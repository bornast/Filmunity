import { Component, OnInit, ViewEncapsulation, Input, Output, EventEmitter } from '@angular/core';
import { FilmService } from 'src/app/_services/film.service';
import { RecordName } from 'src/app/_models/recordName';
import { filter } from 'rxjs/operators';

@Component({
	selector: 'sidebar-layout-one',
	templateUrl: './SidebarLayoutOne.component.html',
	styleUrls: ['./SidebarLayoutOne.component.scss'],
	encapsulation: ViewEncapsulation.None
})
export class SidebarLayoutOneComponent implements OnInit {

	orderByOptions: any[];
	selectedOrderByOption: string;
	
	genreOptions: RecordName[];
	selectedGenreOption: string;

	@Input() searchTxt: string;
	@Output() filters = new EventEmitter();

	constructor(private filmService: FilmService) { }

	ngOnInit() {
		this.loadOrderByOptions();
		this.loadGenres();
	}

	loadOrderByOptions() {
		this.orderByOptions = [
			{
				id: "default", name: "Default Order", orderBy: null
			},
			{
				id: "highestRated", name: "Highest rated", orderBy: "Rating"
			},
			{
				id: "newest", name: "Newest", orderBy: "Year"
			}
		];

		this.selectedOrderByOption = "default";
	}

	loadGenres() {
		this.filmService.getGenres().subscribe((genres) => {
			this.genreOptions = genres;
			this.selectedGenreOption = "default";
		});
	}

	filter() {

		let filters = {
			orderBy: this.orderByOptions.filter(orderByOption => { return orderByOption.id === this.selectedOrderByOption})[0].orderBy,
			genreId: this.selectedGenreOption == "default" ? null : this.selectedGenreOption,
			searchTxt: this.searchTxt
		};

		console.log("filtering", filters);

		this.filters.emit(filters);
	}

}
