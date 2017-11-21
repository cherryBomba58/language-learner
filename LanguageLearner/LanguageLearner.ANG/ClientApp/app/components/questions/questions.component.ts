import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { CourseService } from '../../services/course.service';
import { Course } from "../../classes/course";

@Component({
    selector: 'questions',
    templateUrl: './questions.component.html',
    styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit {
    courses: Observable<Course[]>;

    constructor(private courseService: CourseService) { }

    ngOnInit() {
        this.getCourses();
    }

    getCourses() {
        this.courses = this.courseService.getCourses();
    }
}