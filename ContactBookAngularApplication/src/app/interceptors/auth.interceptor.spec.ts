import { TestBed } from '@angular/core/testing';

import { AuthInterceptor } from './auth.interceptor';
import { HttpClientModule } from '@angular/common/http';

describe('AuthInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[HttpClientModule],
    providers: [
      AuthInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: AuthInterceptor = TestBed.inject(AuthInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
