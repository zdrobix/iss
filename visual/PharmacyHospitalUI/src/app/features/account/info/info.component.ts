import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { UserDTO } from '../../models/user-dto.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent implements OnInit {
  user$: Observable<UserDTO | null> | undefined;

  constructor(private loginService: LoginService) {}

  ngOnInit() {
    this.user$ = this.loginService.user$;
  }
}
