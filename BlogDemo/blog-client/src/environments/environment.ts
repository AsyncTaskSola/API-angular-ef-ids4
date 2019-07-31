// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,//生产环境
  // apiUrlBase:"https://localhost:6001/api",
  apiUrlBase:"/api", //配置了proxy.conf.js


  //客户端的配置
  openIdConnectSettting:{
    authority:'https:localhost:5001/',//idp
    client_id: 'blog-client',
    redirect_uri: 'http://localhost:4200/signin-oidc',//登陆成功跳转到这
    scope:"openid profile restapi email",
    response_type :"id_token token",
    post_logout_redirect_uri: 'http://localhost:4200/',//登出返回到默认地址
    automaticSilentRenew: true,//automaticSilentRenew用于静默身份验证。 当令牌过期时，请求将被发送到服务器，
    //并且只要Idsrv上的会话处于活动状态，令牌就会自动更新而无需用户交互（不需要在登录页面上键入凭据）。
    silent_redirect_uri: 'http://localhost:4200/redirect-silentrenew'//重新获取和上面配合来的
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
