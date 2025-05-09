import { Injectable } from '@angular/core';
import { CanActivate, CanActivateFn, Router, UrlTree } from '@angular/router';
import { Observable, map } from 'rxjs';
import { LoginService } from 'src/app/features/account/services/login.service';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  constructor(private loginService: LoginService, private router: Router) {}

  /*
  Some routes are restricted, and can't be visited unless the user is logged in.
  If the user is logged in, this method returns an Observable, otherwise a tree, redirecting the user.
  */
  canActivate(): Observable<boolean | UrlTree> {
    return this.loginService.getLoggedInUser().pipe(
      map(user => user ? true : this.router.createUrlTree(['/']))
    );
  }
}