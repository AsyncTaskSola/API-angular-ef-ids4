import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SigninOidcComponent } from './shared/oidc/signin-oidc/signin-oidc.component';
import { RedirectSilentRenewComponent } from './shared/oidc/redirect-silent-renew/redirect-silent-renew.component';

const routes: Routes = [
  {
    path:'blog',loadChildren:'./blog/blog.module#BlogModule'
  },
  {
    path:'signin-oidc',component:SigninOidcComponent
  },
  {
    path:'redirect-silent-renew',component:RedirectSilentRenewComponent
  },
  {
    path:'**',redirectTo:'blog'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
