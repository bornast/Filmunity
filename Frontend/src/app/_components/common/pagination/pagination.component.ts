import { Pagination } from 'src/app/_models/pagination';
import { ViewEncapsulation, EventEmitter, Output, Input, SimpleChanges, OnInit, Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
	selector: 'pagination',
	templateUrl: './pagination.component.html',
	styleUrls: ['./pagination.component.scss'],
	encapsulation: ViewEncapsulation.None
})
export class PaginationComponent implements OnInit {

	@Output() pageChanged = new EventEmitter();
	@Input() pagination: Pagination;
	paginationOptions: string[];
	selectedPaginationOption: string;

	constructor() { }	

	ngOnInit() {
		this.loadPagination();
	}	

	ngOnChanges(changes: SimpleChanges) {
		if (!changes.pagination.firstChange)
        	this.loadPagination();        
    }

	loadPagination() {
		if (this.pagination != null) {
			this.paginationOptions = this.calculatePages(this.pagination.currentPage, this.pagination.totalPages);
			this.selectedPaginationOption = this.pagination.currentPage.toString();
		}
	}

	calculatePages(currentPage, totalPages) {
		var current = currentPage,
			last = totalPages,
			delta = 2,
			left = current - delta,
			right = current + delta + 1,
			range = [],
			rangeWithDots = [],
			l;

		for (let i = 1; i <= last; i++) {
			if (i == 1 || i == last || i >= left && i < right) {
				range.push(i);
			}
		}

		for (let i of range) {
			if (l) {
				if (i - l === 2) {
					rangeWithDots.push(l + 1);
				} else if (i - l !== 1) {
					rangeWithDots.push('...');
				}
			}
			rangeWithDots.push(i);
			l = i;
		}

		return rangeWithDots;
	}

	changePage(pageNumber: any) {
		if (pageNumber === "...") {
			for (let i = 0; i < this.paginationOptions.length; i++) {
				if (this.paginationOptions[i] === pageNumber) {
					pageNumber = this.selectedPaginationOption > this.paginationOptions[i-1] ? Number(this.paginationOptions[i+1]) - 1 : Number(this.paginationOptions[i-1]) + 1;
					break;
				}
			}
		}
		this.pageChanged.emit(pageNumber);
	}

}
