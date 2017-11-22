import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { Course } from "../../classes/course";
import { QuestionType } from "../../classes/questionType";
import { Learnable } from "../../classes/learnable";

import { QuestionTypeService } from '../../services/questiontype.service';
import { LearnableService } from "../../services/learnable.service";

@Component({
    selector: 'task',
    templateUrl: './task.component.html'
})
export class TaskComponent implements OnInit {
    courseID: number;
    qtype: QuestionType;
    learnablelist: Observable<Learnable[]>;

    constructor(
        private route: ActivatedRoute,
        private questionTypeService: QuestionTypeService,
        private learnableService: LearnableService
    ) { }

    ngOnInit() {
        this.courseID = +this.route.snapshot.params['courseid'];
        this.qtype = this.questionTypeService.getQuestionType();
        this.getLearnables();
    }

    getLearnables() {
        this.learnablelist = this.learnableService.getLearnables(this.courseID);
    }
}