import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // Input parameters are used for Parent to child communication, in the scenario when Parent component sends some data down to child component we use Input Property
  @Input() valuesFromHome: any;
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  constructor() { }

  ngOnInit() {
  }

  register() {
    console.log(this.model);
  }

  cancel() {
    //This is the event we call in the parent
    this.cancelRegister.emit(false);
  }

}
