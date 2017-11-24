import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { Course } from "../../classes/course";
import { QuestionType } from "../../classes/questionType";
import { QuestionTypesModel } from "../../classes/questionTypesModel";

import { CourseService } from '../../services/course.service';
import { SharedDataService } from '../../services/shared-data.service';

@Component({
    selector: 'questions',
    templateUrl: './questions.component.html',
    styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit {
    courses: Observable<Course[]>;
    qtype: QuestionType = QuestionType.ToEnglish;

    qmodels: QuestionTypesModel[] = [
        new QuestionTypesModel(QuestionType.ToEnglish, 'Magyarról angolra fordítás'),
        new QuestionTypesModel(QuestionType.ToHungarian, 'Angolról magyarra fordítás'),
        new QuestionTypesModel(QuestionType.ToImages, 'Képekhez szavak írása'),
        new QuestionTypesModel(QuestionType.RightWord, 'Helyes szó kiválasztása egy listáról')
    ];

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

    startQuestions(courseID: number, courseName: string) {
        this.sharedDataService.setQuestionType(this.qtype);
        this.sharedDataService.setCourseName(courseName);
        this.router.navigate(['/task', courseID]);
    }
}