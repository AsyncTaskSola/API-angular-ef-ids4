import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BlogRoutingModule } from './blog-routing.module';
import { MaterialModule } from '../shared/material/material.module';
import { BlogAppComponent } from './blog-app.component';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { ToolbarComponent } from './components/toolbar/toolbar.component';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { PostService } from './services/post.service';
import { PostListComponent } from './components/post-list/post-list.component';
import { AuthorizationHeaderInterceptor } from '../shared/oidc/authorization-header-interceptor.interceptor.ts';
import { PostCardComponent } from './components/post-card/post-card.component';
import { WritePostComponent } from './components/write-post/write-post.component';
import { TinymceService } from './services/tinymce.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditorModule } from '@tinymce/tinymce-angular';


@NgModule({
  imports: [
    CommonModule,
    BlogRoutingModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    EditorModule
  ],
  declarations:
   [BlogAppComponent
    , SidenavComponent, ToolbarComponent, PostListComponent, PostCardComponent, WritePostComponent],

    providers: [
      PostService,
      TinymceService,
      {
        provide: HTTP_INTERCEPTORS,
        useClass: AuthorizationHeaderInterceptor,
        multi: true
      }
    ]
})
export class BlogModule { }
