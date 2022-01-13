import { Injectable } from "@angular/core";
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from "rxjs";
import { ApiAuthService } from "../services/apiauth.service";

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private apiauthService: ApiAuthService) {

    }

    canActivate(route: ActivatedRouteSnapshot) {
        const usuario = this.apiauthService.usuarioData;
        if (usuario) {
            return true;
        }
        this.router.navigate(['/login']);
        return false;
    }

}