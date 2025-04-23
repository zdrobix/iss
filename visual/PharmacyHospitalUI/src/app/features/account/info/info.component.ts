import { Component, OnDestroy, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { UserDTO } from '../../models/user-dto.model';
import { Observable, Subscription, take } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent implements OnInit, OnDestroy {
  user$: Observable<UserDTO | null> | undefined;
  private logoutSubscription?: Subscription;

  constructor(private loginService: LoginService, private router: Router) {}

  ngOnInit() {
    this.user$ = this.loginService.user$;
  }

  logout(){
    this.logoutSubscription = this.loginService.logout().pipe(take(1)).subscribe({
      next: () => {
        this.router.navigate(['/login']);
      }
    });
  }

  ngOnDestroy() {
      this.logoutSubscription?.unsubscribe();
  }
}
