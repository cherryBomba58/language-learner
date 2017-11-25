import { Component } from '@angular/core';

import { AccountService } from '../../services/account.service';

import { Regist } from '../../classes/regist';

@Component({
    selector: 'regist',
    templateUrl: './regist.component.html'
})
export class RegistComponent {
    regist: Regist = new Regist("", "", "", "", "");
    message: any = null;

    constructor(private accountService: AccountService) { }

    registrate() {
        this.accountService.registrate(this.regist)
            .subscribe((res: any) => {
                this.message = res;
                if (!this.message.succeeded && !this.message.errors) {
                    this.message.succeeded = false;
                    this.message.errors = [{ description: "Something's wrong, for example, the two passwords are not the same, or they are too short, or some data is missing, or the email format is not valid." }];
                }
            });
    }
}