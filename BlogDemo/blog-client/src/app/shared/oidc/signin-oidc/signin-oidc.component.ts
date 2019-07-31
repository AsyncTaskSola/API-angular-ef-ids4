import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OpenIdConnectService } from '../open-id-connect.service';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-signin-oidc',
  templateUrl: './signin-oidc.component.html',
  styleUrls: ['./signin-oidc.component.scss']
})
export class SigninOidcComponent implements OnInit {

  constructor(private openIdConnectService: OpenIdConnectService,
    private router: Router) { }

  ngOnInit() {
    this.openIdConnectService.userloaded$.subscribe((userLoaded) => { //ids4登陆页已经执行完了
      if (userLoaded) {
        this.router.navigate(['./']);//导航到首页得路由
      } else {
        if (!environment.production) {
          console.log('An error happened: user wasn\'t loaded.');
        }
      }
    });

    this.openIdConnectService.handleCallback();
  }
}
