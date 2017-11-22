import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { Learnable } from "../classes/learnable";

@Injectable()
export class LearnableService {
    private url = 'api/learnable';  // URL to web api

    constructor(private http: Http) { }

    getLearnables(courseID: number): Observable<Learnable[]> {
        let myUrl: string = `${this.url}/${courseID}`;
        return this.http
            .get(myUrl)
            .map(res => res.json())
            .catch(this.handleError);
    }

    private handleError(error: Response | any): Observable<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Observable.throw(error.message || error);
    }
}