import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Observable } from 'rxjs/Observable';

import { Course } from "../../classes/course";
import { QuestionType } from "../../classes/questionType";
import { Learnable } from "../../classes/learnable";

import { SharedDataService } from '../../services/shared-data.service';
import { LearnableService } from "../../services/learnable.service";

@Component({
    selector: 'task',
    templateUrl: './task.component.html'
})
export class TaskComponent implements OnInit {
    courseID: number;
    courseName: string;
    qtype: QuestionType;
    learnablelist: Observable<Learnable[]>;

    constructor(
        private route: ActivatedRoute,
        private sharedDataService: SharedDataService,
        private learnableService: LearnableService
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
}