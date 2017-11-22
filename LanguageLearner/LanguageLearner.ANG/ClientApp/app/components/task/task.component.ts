import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { Course } from "../../classes/course";
import { QuestionType } from "../../classes/questionType";

import { QuestionTypeService } from '../../services/questiontype.service';

@Component({
    selector: 'task',
    templateUrl: './task.component.html'
})
export class TaskComponent implements OnInit {
    courseID: number;
    qtype: QuestionType;

    constructor(
        private route: ActivatedRoute,
        private questionTypeService: QuestionTypeService
    ) { }

    ngOnInit() {
        this.courseID = +this.route.snapshot.params['courseid'];
        this.qtype = this.questionTypeService.getQuestionType();
    }
}