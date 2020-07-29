import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DropzoneModule } from 'ngx-dropzone-wrapper';
import { DROPZONE_CONFIG } from 'ngx-dropzone-wrapper';
import { DropzoneConfigInterface } from 'ngx-dropzone-wrapper';

import { ListComponent } from './List/List.component';
import { FilmEditorComponent } from './film-editor/film-editor.component';

import { AdminRoutes } from './admin.routing';
import { GlobalModule } from '../globalFrontendComponents/global.module';
import { FormsModule } from '@angular/forms';

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
    ListComponent,
    FilmEditorComponent
  ],
  providers: [
   {
     provide: DROPZONE_CONFIG,
     useValue: DEFAULT_DROPZONE_CONFIG
   }
 ]
})

export class AdminModule {}
