import { Component, OnDestroy, OnInit } from '@angular/core';
import { LoginRequest } from '../../models/login-request.model';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';
import { UserDTO } from '../../models/user-dto.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy  {
  model: LoginRequest;
  private addCategorySubscription?: Subscription;

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
    this.addCategorySubscription = this.loginService.login(this.model).subscribe({
      next: (response: UserDTO) => {
        this.loginService.setLoggedInUser(response);
        this.router.navigate(['/account']);
      },
      error: (error) => {}
      });
  }

  ngOnDestroy(): void {
    this.addCategorySubscription?.unsubscribe();
  }
}
