import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AdministratorGuard implements CanActivate {
  canActivate(): boolean {
    let token = localStorage.getItem("token") ?? "";
    if (token == ""){
      alert('Debe iniciar sesión para acceder a esta página.');
      this.router.navigate(['/home']);
      return false;
    }
    return true;
  }
  constructor(private router: Router) { }
}
