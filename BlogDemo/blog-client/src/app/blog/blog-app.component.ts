import { Component, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-blog-app',
  template: `<app-sidenav></app-sidenav>`,
  styles: []
})
export class BlogAppComponent implements OnInit {

  //只是引入一个图标
  constructor(iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
    iconRegistry.addSvgIcon('more_vert', sanitizer.bypassSecurityTrustResourceUrl('/assets/baseline-more_vert-24px.svg'));
    iconRegistry.addSvgIcon('baseline-menu', sanitizer.bypassSecurityTrustResourceUrl('/assets/baseline-menu-24px.svg'));
  }

  ngOnInit() {
  }

}
