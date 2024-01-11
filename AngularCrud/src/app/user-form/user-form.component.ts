import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css'],
})
export class UserFormComponent implements OnInit {
  user: any = {};

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    const userId = this.route.snapshot.paramMap.get('id');

    if (userId) {
      this.userService.getUserById(+userId).subscribe((user) => {
        this.user = user;
      });
    }
  }

  saveUser(): void {
    if (this.user.id) {
      this.userService.updateUser(this.user).subscribe(() => {
        this.router.navigate(['/']); 
      });
    } else {
      this.userService.addUser(this.user).subscribe(() => {
        this.router.navigate(['/']); 
      });
    }
  }
}
