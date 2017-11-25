import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';

import { AccountService } from '../../services/account.service';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    @Input() isSignedIn: boolean;

    constructor(
        private router: Router,
        private accountService: AccountService
    ) { }

    logout() {
        this.accountService.logout()
            .subscribe((res: any) => this.router.navigate(['/login']));
    }
}
