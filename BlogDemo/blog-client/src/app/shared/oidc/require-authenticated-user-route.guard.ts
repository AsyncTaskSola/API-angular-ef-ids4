import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { promise } from 'protractor';
import { OpenIdConnectService } from './open-id-connect.service';


@Injectable({
  providedIn: 'root'
})
//https://angular.io/api/router/CanActivate
export class RequireAuthenticatedUserRouteGuard implements CanActivate {

constructor(
  private OpenIdConnectService:OpenIdConnectService,
  private router:Router){}



  canActivate(
    next:ActivatedRouteSnapshot,
    state:RouterStateSnapshot):Observable<boolean>|Promise<boolean>|boolean{

      if(this.OpenIdConnectService.userAvailable)
      {
      return true;//有权限
      }
      else{
        //在返回false之前触发一些登陆，没有反问权限跳转到ids4的登陆页面
        this.OpenIdConnectService.triggerSignIn();
        return false;
      }
    }
  }

