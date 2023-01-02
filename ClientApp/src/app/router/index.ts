import { RouterModule } from '@angular/router';
import { HomeComponent } from '../pages/home/home.component';
import { LoginPageComponent } from '../pages/login-page/login-page.component';
import { RegistrationPageComponent } from '../pages/registration-page/registration-page.component';
import TableUrlsViewComponent from '../pages/table-urls/table-urls-view.component';

const routes = [
  { path: '', component: HomeComponent },
  { path: 'tableUrl', component: TableUrlsViewComponent },
  { path: 'loginPage', component: LoginPageComponent },
  { path: 'registrationPage', component: RegistrationPageComponent },
];

const router = RouterModule.forRoot(routes, {
  useHash: false,
});

export default router;
