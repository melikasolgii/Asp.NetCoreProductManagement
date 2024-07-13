import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideHttpClient } from '@angular/common/http';

//adding toaster npm
import { provideToastr } from 'ngx-toastr';

export const appConfig: ApplicationConfig = {
  providers: [
     provideRouter(routes),
     provideAnimationsAsync(),
     provideHttpClient(),
     provideToastr({
      timeOut:1500,
      progressAnimation:'decreasing',
      progressBar:true,
      closeButton :true,
      positionClass: 'toast-bottom-left',
      easing: 'ease-in'
     })
    ]
};
