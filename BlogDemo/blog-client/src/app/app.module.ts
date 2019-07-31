import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';

import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule, MatCheckboxModule} from '@angular/material';


import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { RequireAuthenticatedUserRouteGuard } from './shared/oidc/require-authenticated-user-route.guard';
import { OpenIdConnectService } from './shared/oidc/open-id-connect.service';
import { SigninOidcComponent } from './shared/oidc/signin-oidc/signin-oidc.component';
import { RedirectSilentRenewComponent } from './shared/oidc/redirect-silent-renew/redirect-silent-renew.component';

@NgModule({
  declarations: [
    AppComponent,
    SigninOidcComponent,
    RedirectSilentRenewComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule
    // MatButtonModule,
    // MatCheckboxModule

  ],
  providers: [
    OpenIdConnectService,
    RequireAuthenticatedUserRouteGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
