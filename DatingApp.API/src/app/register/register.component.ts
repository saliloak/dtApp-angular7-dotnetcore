import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from "../_services/alertify.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // Input parameters are used for Parent to child communication, in the scenario when Parent component sends some data down to child component we use Input Property
  //@Input() valuesFromHome: any;
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      this.alertify.success('Registration Successful');
    },
      error => {
        this.alertify.error(error);
      });
  }

  cancel() {
    //This is the event we call in the parent
    this.cancelRegister.emit(false);
  }

}
