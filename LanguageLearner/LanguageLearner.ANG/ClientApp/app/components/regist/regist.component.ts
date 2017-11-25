﻿import { Component } from '@angular/core';

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
                    this.message.errors = [{ description: "Valami nem jó, például a két jelszó nem egyezik meg, vagy túl rövidek, vagy néhány adat hiányzik, vagy az email formátuma nem helyes." }];
                }
            });
    }
}