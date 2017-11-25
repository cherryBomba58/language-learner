import { Component } from '@angular/core';

import { AccountService } from '../../services/account.service';
import { IdentityUserModel } from "../../classes/identityUserModel";

@Component({
    selector: 'app',
    templateUrl: './app.component.html'
})
export class AppComponent {
    isSignedIn: boolean = false;

    constructor(
        private accountService: AccountService
    ) { }

    getIdentity(event: any) {
        this.accountService.getIdentity()
            .subscribe((model: IdentityUserModel) => this.isSignedIn = model.isSignedIn);
    }
}
