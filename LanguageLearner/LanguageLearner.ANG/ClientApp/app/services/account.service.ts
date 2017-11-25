﻿import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { Regist } from "../classes/regist";
import { Login } from "../classes/login";

@Injectable()
export class AccountService {
    private headers: Headers = new Headers({ 'Content-Type': 'application/json' });
    private url = 'api/account';  // URL to web api

    constructor(private http: Http) { }

    registrate(reg: Regist): Observable<any> {
        const myUrl: string = `${this.url}/register`;
        return this.http
            .post(myUrl, JSON.stringify({ FullName: reg.fullName, Email: reg.email, UserName: reg.userName, Password: reg.password, ConfirmPassword: reg.confirmPassword }), { headers: this.headers })
            .map(res => res.json())
            .catch(this.handleError);
    }

    login(log: Login): Observable<any> {
        const myUrl: string = `${this.url}/login`;
        return this.http
            .post(myUrl, JSON.stringify({ UserName: log.userName, Password: log.password, RememberMe: log.rememberMe }), { headers: this.headers })
            .map(res => res.json())
            .catch(this.handleError);
    }

    logout(): Observable<any> {
        const myUrl: string = `${this.url}/logout`;
        return this.http
            .post(myUrl, JSON.stringify({ }), { headers: this.headers })
            .catch(this.handleError);
    }

    private handleError(error: Response | any): Observable<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Observable.throw(error.message || error);
    }
}