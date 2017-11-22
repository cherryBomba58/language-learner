import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { Course } from "../../classes/course";
import { QuestionType } from "../../classes/questionType";

import { CourseService } from '../../services/course.service';
import { QuestionTypeService } from '../../services/questiontype.service';

@Component({
    selector: 'test',
    templateUrl: './test.component.html'
})
export class TestComponent implements OnInit {
    courses: Observable<Course[]>;

    constructor(
        private router: Router,
        private courseService: CourseService,
        private questionTypeService: QuestionTypeService
    ) { }

    ngOnInit() {
        this.getCourses();
    }

    getCourses() {
        this.courses = this.courseService.getCourses();
    }

    startTest(courseID: number) {
        this.questionTypeService.setQuestionType(QuestionType.Test);
        this.router.navigate(['/task', courseID]);
    }
}