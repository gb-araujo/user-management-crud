
import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css'],
})
export class UserFormComponent implements OnInit {
  user: any = {};

  constructor(private userService: UserService) {}

  ngOnInit(): void {}

  saveUser(): void {
    if (this.user.id) {
      this.userService.updateUser(this.user.id, this.user).subscribe(() => {
        alert("Usuario atualizado")
      });
    } else {
      this.userService.addUser(this.user).subscribe(() => {
        alert("Usuario adicionado")
        window.location.href = '/';
      });
    }
  }
}
