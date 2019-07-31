import { Injectable } from '@angular/core';
import { UserManager, User } from 'oidc-client';
import { environment } from 'src/environments/environment';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

//https://www.cnblogs.com/fuyouchen/p/9603474.html
export class OpenIdConnectService {
  private userManager: UserManager = new UserManager(environment.openIdConnectSettting);
  private currentUser: User;//当前用户

  userloaded$ = new ReplaySubject<boolean>(1);//这里直接抄写就行了rxjs
  get userAvailable(): boolean//用户可用.当前用户是否登陆了
  {
    return this.currentUser != null;
  }
  get user(): User {
    return this.currentUser;
  }
  constructor() {
    //重构，刚刚进来应该把状态先清理一下
    this.userManager.clearStaleState();

    //两个事件处理一下
    //登陆成功 
    this.userManager.events.addUserLoaded(user => {
      if (!environment.production) {
        console.log("User loaded", user);
      }
      this.currentUser = user;
      this.userloaded$.next(true);//广播
    });
    //登出操作
    this.userManager.events.addUserUnloaded((e) => {
      if (!environment.production) {
        console.log("user unloaded");
      }
      this.currentUser = null;
      this.userloaded$.next(false);
    });
  }
//重定向到登录触发
  triggerSignIn() {
    this.userManager.signinRedirect().then(() => {
      if (!environment.production) {
        console.log('Redirection to signin triggered.');
      }
    });
  }

  handleCallback() {
    this.userManager.signinRedirectCallback().then(user => {
      if (!environment.production) {
        console.log('Callback after signin handled. 登录后回调', user);//登录后回调
      }
    });
  }

  handleSilentCallback() {
    this.userManager.signinSilentCallback().then(user => {
      this.currentUser = user;
      if (!environment.production) {
        console.log('Callback after silent signin handled.', user);
      }
    });
  }

  triggerSignOut() {
    this.userManager.signoutRedirect().then(resp => {
      if (!environment.production) {
        console.log('Redirection to sign out triggered.', resp);
      }
    });
  }
}
