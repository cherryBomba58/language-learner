import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AccountService } from '../../services/account.service';
import { IdentityUserModel } from "../../classes/identityUserModel";

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent implements OnInit {
    isSignedIn: boolean = false;

    constructor(
        private router: Router,
        private accountService: AccountService
    ) { }

    ngOnInit(): void {
        this.getIdentity();
    }

    getIdentity() {
        this.accountService.getIdentity()
            .subscribe((model: IdentityUserModel) => this.isSignedIn = model.isSignedIn);
    }

    logout() {
        this.accountService.logout()
            .subscribe((res: any) => this.router.navigate(['/home']));
    }
}
