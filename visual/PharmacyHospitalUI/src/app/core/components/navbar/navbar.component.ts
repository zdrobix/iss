import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginService } from 'src/app/features/account/services/login.service';
import { User } from 'src/app/features/models/user.model';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  user$: Observable<User | null>;

  constructor(private loginService: LoginService ) {
    this.user$ = this.loginService.getLoggedInUser();
   }

  isAdmin(user: User| null): boolean {
    return user?.role === "ADMIN";
  }

}
