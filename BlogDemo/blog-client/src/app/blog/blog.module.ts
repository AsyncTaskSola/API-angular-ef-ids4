import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BlogRoutingModule } from './blog-routing.module';
import { MaterialModule } from '../shared/material/material.module';
import { BlogAppComponent } from './blog-app.component';
import { SidenavComponent } from './components/sidenav/sidenav.component';

@NgModule({
  imports: [
    CommonModule,
    BlogRoutingModule,
    MaterialModule
  ],
  declarations:
   [BlogAppComponent
    , SidenavComponent]
})
export class BlogModule { }
