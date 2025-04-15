import { Injectable } from '@angular/core';
import { CanActivate, CanActivateFn, Router, UrlTree } from '@angular/router';
import { Observable, map } from 'rxjs';
import { LoginService } from 'src/app/features/account/services/login.service';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  constructor(private loginService: LoginService, private router: Router) {}

  canActivate(): Observable<boolean | UrlTree> {
    return this.loginService.getLoggedInUser().pipe(
      map(user => user ? true : this.router.createUrlTree(['/']))
    );
  }
}