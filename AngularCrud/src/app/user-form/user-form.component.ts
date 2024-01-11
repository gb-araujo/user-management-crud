// user-form.component.ts
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
      // Load user details if ID is present (for edit page)
      this.userService.getUserById(+userId).subscribe((user) => {
        this.user = user;
      });
    }
  }

  saveUser(): void {
    if (this.user.id) {
      // If user has an ID, it's an update
      this.userService.updateUser(this.user).subscribe(() => {
        this.router.navigate(['/']); // Redirect to user list after update
      });
    } else {
      // If user has no ID, it's an add
      this.userService.addUser(this.user).subscribe(() => {
        this.router.navigate(['/']); // Redirect to user list after add
      });
    }
  }
}
