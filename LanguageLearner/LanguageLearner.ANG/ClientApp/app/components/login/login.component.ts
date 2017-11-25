import { Component } from '@angular/core';

import { AccountService } from '../../services/account.service';

import { Login } from '../../classes/login';

@Component({
    selector: 'login',
    templateUrl: './login.component.html'
})
export class LoginComponent {
    log: Login = new Login("", "", false);
    message: any = null;

    constructor(private accountService: AccountService) { }

    login() {
        this.accountService.login(this.log)
            .subscribe((res: any) => {
                this.message = res;
                if (!this.message.succeeded && !this.message.description) {
                    this.message.description = "A felhasználónév vagy a jelszó rossz.";
                }
            });
    }
}