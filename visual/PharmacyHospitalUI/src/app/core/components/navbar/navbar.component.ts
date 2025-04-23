import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginService } from 'src/app/features/account/services/login.service';
import { UserDTO } from 'src/app/features/models/user-dto.model';
import { User } from 'src/app/features/models/user.model';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  user$: Observable<UserDTO | null>;

  constructor(private loginService: LoginService ) {
    this.user$ = this.loginService.getLoggedInUser();
   }

  isAdmin(user: UserDTO | null): boolean {
    return user?.role === "ADMIN";
  }

}
