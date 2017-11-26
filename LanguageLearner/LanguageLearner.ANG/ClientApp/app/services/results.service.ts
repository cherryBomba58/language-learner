import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { Answer } from "../classes/answer";

@Injectable()
export class ResultsService {
    private url = 'api/results';  // URL to web api
    private headers: Headers = new Headers({ 'Content-Type': 'application/json' });

    constructor(private http: Http) { }

    createResults(courseID: number, answers: Answer[]): Observable<any> {
        return this.http
            .post(this.url, JSON.stringify({ CourseID: courseID, AnswerList: answers }), { headers: this.headers })
            .map(res => res.json())
            .catch(this.handleError);
    }

    private handleError(error: Response | any): Observable<any> {
        console.error('An error occurred', error);
        return Observable.throw(error.message || error);
    }
}