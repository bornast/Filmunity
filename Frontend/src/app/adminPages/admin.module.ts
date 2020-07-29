import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DropzoneModule } from 'ngx-dropzone-wrapper';
import { DROPZONE_CONFIG } from 'ngx-dropzone-wrapper';
import { DropzoneConfigInterface } from 'ngx-dropzone-wrapper';

import { FilmListComponent } from './film/film-list/film-list.component';
import { FilmEditorComponent } from './film/film-editor/film-editor.component';

import { AdminRoutes } from './admin.routing';
import { GlobalModule } from '../globalFrontendComponents/global.module';
import { FormsModule } from '@angular/forms';
import { ParticipantListComponent } from './participant/participant-list/participant-list.component';
import { ParticipantEditorComponent } from './participant/participant-editor/participant-editor.component';
import { UserListComponent } from './user/user-list/user-list.component';
import { UserEditorComponent } from './user/user-editor/user-editor.component';

const DEFAULT_DROPZONE_CONFIG: DropzoneConfigInterface = {
   // Change this to your upload POST address:
    url: 'https://httpbin.org/post',
    maxFilesize: 50,
    acceptedFiles: 'image/*'
  };


@NgModule({
  imports: [
    CommonModule,
    DropzoneModule,
	RouterModule.forChild(AdminRoutes),
	GlobalModule,
	FormsModule
  ],
  declarations: [ 
    FilmListComponent,
	FilmEditorComponent,
	ParticipantListComponent,
	ParticipantEditorComponent,
	UserListComponent,
	UserEditorComponent
  ],
  providers: [
   {
     provide: DROPZONE_CONFIG,
     useValue: DEFAULT_DROPZONE_CONFIG
   }
 ]
})

export class AdminModule {}
