import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BlogAppComponent } from './blog-app.component';

const routes: Routes = [
  {path:'',component:BlogAppComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BlogRoutingModule { }
