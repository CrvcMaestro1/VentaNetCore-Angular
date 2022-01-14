import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ApiAuthService } from "../services/apiauth.service";
import { FormBuilder, Validators } from "@angular/forms";

@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {

    public loginForm = this.formBuilder.group({
        email: ['', Validators.required],
        password: ['', Validators.required]
    })

    constructor(
        public apiauthService: ApiAuthService,
        private router: Router,
        private formBuilder: FormBuilder
    ) {
        if (this.apiauthService.usuarioData) {
            this.router.navigate(['/']);
        }
    }

    ngOnInit(): void {

    }

    login() {
        this.apiauthService.login(this.loginForm.value).subscribe(response => {
            if (response.exito === 1) {
                this.router.navigate(['/']);
            }
        });
    }
}