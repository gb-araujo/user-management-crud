
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { UserService } from '../user.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css'],
})
export class UserDetailsComponent implements OnInit {
  user: any;
  isEditing: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.getUserDetails();
  }

  getUserDetails(): void {
    this.route.params.subscribe((params: Params) => {
      const userId = +params['id']; // convert to number
      this.userService.getUserById(userId).subscribe((user) => {
        this.user = user;
      });
    });
  }

  editUser(): void {
    this.isEditing = true;
  }

  goBack(): void {
    this.location.back();
  }
}
