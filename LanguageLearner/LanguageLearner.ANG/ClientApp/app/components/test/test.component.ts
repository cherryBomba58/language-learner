import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { Course } from "../../classes/course";
import { QuestionType } from "../../classes/questionType";

import { CourseService } from '../../services/course.service';
import { SharedDataService } from '../../services/shared-data.service';

@Component({
    selector: 'test',
    templateUrl: './test.component.html'
})
export class TestComponent implements OnInit {
    courses: Observable<Course[]>;

    constructor(
        private router: Router,
        private courseService: CourseService,
        private sharedDataService: SharedDataService
    ) { }

    ngOnInit() {
        this.getCourses();
    }

    getCourses() {
        this.courses = this.courseService.getCourses();
    }

    startTest(courseID: number, courseName: string) {
        this.sharedDataService.setQuestionType(QuestionType.Test);
        this.sharedDataService.setCourseName(courseName);
        this.router.navigate(['/task', courseID]);
    }
}