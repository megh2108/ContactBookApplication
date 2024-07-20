import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { map } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  //for inject dependency
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.isAuthenticated().pipe(
    map(isAuthenticated => {
      if (isAuthenticated) {
        return true;
      } else {
        router.navigate(['/signin']);
        return false;
      }

    })
  );
};
