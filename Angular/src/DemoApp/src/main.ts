import { bootstrapApplication } from '@angular/platform-browser';
import { AppInjectorService } from '@common/service/app-injector.service';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';

bootstrapApplication(AppComponent, appConfig)
  .then((appRef) => {
    AppInjectorService.setInjector(appRef.injector);
  })
  .catch((err) => console.error(err));
