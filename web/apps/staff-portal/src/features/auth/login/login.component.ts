import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule],
})

export class LoginComponent {
  private fb = inject(FormBuilder); 
  private router = inject(Router); 

  loginForm: FormGroup;
  isLoading = false;
  errorMessage: string | null = null;

  readonly minUsernameLength = 2;

  constructor() {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(this.minUsernameLength)]],
      password: ['', [Validators.required, Validators.minLength(8)]],
    });
  }

  onSubmit(): void {
    if (this.loginForm.invalid)
      return;

    this.isLoading = true;
    this.errorMessage = null;
  }
}