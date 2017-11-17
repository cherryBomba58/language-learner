import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { Course } from "../classes/course";

@Injectable()
export class CourseService {
    private url = 'api/course';  // URL to web api

    constructor(private http: Http) { }

    getCourses(): Observable<Course[]> {
        return this.http
            .get(this.url)
            .map(res => res.json())
            .catch(this.handleError);
    }

    private handleError(error: Response | any): Observable<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Observable.throw(error.message || error);
    }
}