import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AddUserRequest } from 'src/app/features/models/add-user-request.model';
import { UsersService } from '../services/users.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})

export class AddUserComponent implements OnDestroy {
  model: AddUserRequest;
  private addUserSubscription?: Subscription;

  constructor(private router: Router, private userService: UsersService) {
    this.model = {
      name: '',
      username: '',
      password: '',
      role: ''
    };
  }

  onFormSubmit() {
    if (!this.model.name || !this.model.username || !this.model.password || !this.model.role)
      return;
    this.addUserSubscription = this.userService.addUser(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/pharmacy-hospital/#/users');
      }
    });
  }

  ngOnDestroy(): void {
    this.addUserSubscription?.unsubscribe();
  }

}
