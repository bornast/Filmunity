import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { ToastService } from 'src/app/_services/toast.service';

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

	constructor(private toast: ToastService) { }

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
				// if (photo.isMain) {
				// 	this.authService.changeMemberPhoto(photo.url);
				// 	this.authService.currentUser.photoUrl = photo.url;
				// 	localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
				// }
			}
		};
	}

	setMainPhoto(photo: Photo) {
		// this.userService.setMainPhoto(this.authService.decodedToken.nameid, photo.id).subscribe(() => {
		// 	this.currentMain = this.photos.filter(p => p.isMain === true)[0]; // find the previous main photo and set it to false
		// 	this.currentMain.isMain = false;
		// 	photo.isMain = true; // set the selected main photo to true
		// 	// this.getMemberPhotoChange.emit(photo.url);
		// 	this.authService.changeMemberPhoto(photo.url);
		// 	this.authService.currentUser.photoUrl = photo.url;
		// 	localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
		// }, error => {
		// 	this.alertify.error(error);
		// });		
	}

	deletePhoto(id: number) {
		// this.alertify.confirm('Are you sure you want to delete this photo?', () => {
		// 	this.userService.deletePhoto(this.authService.decodedToken.nameid, id).subscribe(() => {
		// 		this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
		// 		this.alertify.success('Photo has been deleted');
		// 	}, error => {
		// 		this.alertify.error('Failed to delete the photo');
		// 	});
		// });
	}

}