import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';

import { Course } from "../../classes/course";
import { QuestionType } from "../../classes/questionType";
import { Learnable } from "../../classes/learnable";
import { Answer } from "../../classes/answer";

import { SharedDataService } from '../../services/shared-data.service';

@Component({
    selector: 'results',
    templateUrl: './results.component.html'
})
export class ResultsComponent implements OnInit {
    courseID: number;
    courseName: string;
    qtype: QuestionType;
    answerlist: Answer[] = [];

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private sharedDataService: SharedDataService
    ) { }

    ngOnInit() {
        this.courseID = +this.route.snapshot.params['courseid'];
        this.courseName = this.sharedDataService.getCourseName();
        this.qtype = this.sharedDataService.getQuestionType();
        this.answerlist = this.sharedDataService.getAnswers();
    }

    goBack() {
        this.router.navigate(['/questions']);
    }
}