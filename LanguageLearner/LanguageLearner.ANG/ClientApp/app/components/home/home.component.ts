import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { AccountService } from '../../services/account.service';
import { IdentityUserModel } from "../../classes/identityUserModel";

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
    user: IdentityUserModel = new IdentityUserModel(false, "");

    constructor(
        private accountService: AccountService
    ) { }

    ngOnInit(): void {
        this.getIdentity();
    }

    getIdentity() {
        this.accountService.getIdentity()
            .subscribe((user: IdentityUserModel) => this.user = user);
    }
}
