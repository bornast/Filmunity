import { Component } from '@angular/core';

@Component({
  selector: 'app-admin-panel',
  styles: [':host /deep/ .mat-sidenav-content {padding: 0;} .mat-sidenav-container {z-index: 1000}'],
  templateUrl: './admin-panel-layout.component.html'
})
export class AdminPanelLayoutComponent {}
