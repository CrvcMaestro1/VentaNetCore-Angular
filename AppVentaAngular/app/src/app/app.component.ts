import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from './models/usuario';
import { ApiAuthService } from './services/apiauth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';
  usuario!: Usuario;

  constructor(public apiauthService: ApiAuthService, private router: Router) {
    this.apiauthService.usuario.subscribe(res => {
      this.usuario = res;
      console.log('Cambió el objeto: ' + res);
    });
  }

  logout() {
    this.apiauthService.logout();
    this.router.navigate(['/login']);
  }
}
