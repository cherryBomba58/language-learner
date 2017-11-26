import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute, Params } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { Course } from "../../classes/course";
import { QuestionType } from "../../classes/questionType";
import { Learnable } from "../../classes/learnable";
import { Answer } from "../../classes/answer";

import { SharedDataService } from '../../services/shared-data.service';
import { LearnableService } from "../../services/learnable.service";
import { ResultsService } from "../../services/results.service";

@Component({
    selector: 'task',
    templateUrl: './task.component.html'
})
export class TaskComponent implements OnInit {
    courseID: number;
    courseName: string;
    qtype: QuestionType;
    learnablelist: Observable<Learnable[]>;
    answerarray: Answer[] = [];

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private sharedDataService: SharedDataService,
        private learnableService: LearnableService,
        private resultsService: ResultsService
    ) { }

    ngOnInit() {
        this.courseID = +this.route.snapshot.params['courseid'];
        this.qtype = this.sharedDataService.getQuestionType();
        this.getLearnables();
        this.getNameOfCourse();
    }

    getLearnables() {
        this.learnablelist = this.learnableService.getLearnables(this.courseID);
    }

    getNameOfCourse() {
        this.courseName = this.sharedDataService.getCourseName();
    }

    addAnswer(i: number, learnableID: number, goodAnswer: string, event: any) {
        let right = (goodAnswer.valueOf().toLowerCase() === event.valueOf().toLowerCase()) ? true : false;
        this.answerarray[i] = new Answer(learnableID, goodAnswer, event, right);
    }

    stop() {
        this.router.navigate(['/questions']);
    }

    showResults() {
        this.sharedDataService.setAnswers(this.answerarray);
        if (this.qtype == QuestionType.Test) {
            this.resultsService.createResults(this.courseID, this.answerarray)
                .subscribe((res: any) => console.log(res));
        }
        this.router.navigate(['/results', this.courseID]);
    }
}