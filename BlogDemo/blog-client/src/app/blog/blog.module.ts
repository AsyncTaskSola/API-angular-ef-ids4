import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BlogRoutingModule } from './blog-routing.module';
import { MaterialModule } from '../shared/material/material.module';
import { BlogAppComponent } from './blog-app.component';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { ToolbarComponent } from './components/toolbar/toolbar.component';

import { HttpClientModule } from '@angular/common/http';
import { PostService } from './services/post.service';
import { PostListComponent } from './components/post-list/post-list.component';



@NgModule({
  imports: [
    CommonModule,
    BlogRoutingModule,
    MaterialModule,
    HttpClientModule
  ],
  declarations:
   [BlogAppComponent
    , SidenavComponent, ToolbarComponent, PostListComponent],

    providers:[PostService]
})
export class BlogModule { }
