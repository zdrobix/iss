import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { User } from 'src/app/features/models/user.model';
import { UsersService } from '../services/users.service';
import { Subscription } from 'rxjs/internal/Subscription';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})

export class UserListComponent implements OnInit, OnDestroy {

  users$?: Observable<User[]>;
  private deleteUserSubscription?: Subscription;


  constructor(private usersService: UsersService) {

  }
  ngOnInit(): void {
    this.users$ = this.usersService.getUsers();
  }

  deleteUser(id: number) {
    this.deleteUserSubscription = this.usersService.deleteUser(id).subscribe(() => {
      this.users$ = this.usersService.getUsers();
    });
  }

  ngOnDestroy(): void {
    this.deleteUserSubscription?.unsubscribe();
  }
}
