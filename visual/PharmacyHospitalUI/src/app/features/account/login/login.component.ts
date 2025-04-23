import { Component, OnDestroy, OnInit } from '@angular/core';
import { LoginRequest } from '../../models/login-request.model';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy  {
  model: LoginRequest;
  private loginSubscription?: Subscription;

  constructor(private loginService: LoginService,
    private router: Router
  ) {
    this.model = {
      username: '',
      password: ''
    };
  }
  ngOnInit(): void {
    if (this.loginService.user$) {
      this.router.navigate(['/account']);
    }
  }

  onFormSubmit() {
    if (!this.model)
      return;
    this.loginSubscription = this.loginService.login(this.model).subscribe({
      next: (response: User) => {
        this.loginService.setLoggedInUser(response);
        this.router.navigate(['/account']);
      },
      error: (error) => {}
      });
  }

  ngOnDestroy(): void {
    this.loginSubscription?.unsubscribe();
  }
}
