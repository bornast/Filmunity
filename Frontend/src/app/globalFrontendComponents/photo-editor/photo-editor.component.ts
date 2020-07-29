import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { ToastService } from 'src/app/_services/toast.service';
import { FilmService } from 'src/app/_services/film.service';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

	@Input() entityTypeId: any;
	@Input() entityId: any;
	@Input() photos: Photo[];
	@Output() getMemberPhotoChange = new EventEmitter<string>();
	uploader: FileUploader;
	hasBaseDropZoneOver = false;
	baseUrl = environment.apiUrl;
	currentMain: Photo;

	constructor(private toast: ToastService, private filmService: FilmService) { }

	ngOnInit() {
		this.initializeUploader();
	}

	fileOverBase(e: any): void {
		this.hasBaseDropZoneOver = e;
	}

	initializeUploader() {
		this.uploader = new FileUploader({
			url: this.baseUrl + 'photo/upload',
			authToken: 'Bearer ' + localStorage.getItem('filmunity-token'),
			isHTML5: true,
			allowedFileType: ['image'],
			removeAfterUpload: true,
			autoUpload: false,
			maxFileSize: 10 * 1024 * 1024,
			additionalParameter: {
				EntityTypeId: this.entityTypeId,
				EntityId: this.entityId 
			}
		});

		this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };

		this.uploader.onSuccessItem = (item, response, status, headers) => {
			if (response) {
				const res: Photo = JSON.parse(response);
				const photo = {
					id: res.id,
					url: res.url,
					description: res.description,
					isMain: res.isMain,
				};
				this.photos.push(photo);
			}
		};
	}

	setMainPhoto(photo: Photo) {
		this.filmService.setMainPhoto(photo.id).subscribe(() => {
			this.currentMain = this.photos.filter(p => p.isMain === true)[0]; // find the previous main photo and set it to false
			this.currentMain.isMain = false;
			photo.isMain = true; // set the selected main photo to true
		}, () => {
			this.toast.error("Failed to set the photo as main!");
		});		
	}

	deletePhoto(id: number) {		
		this.filmService.deletePhoto(id).subscribe(() => {
			this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
			this.toast.success('Photo has been deleted');
		}, () => {
			this.toast.error('Failed to delete the photo');
		});
	}

}
