import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { QuestionType } from "../classes/questionType";
import { Answer } from "../classes/answer";

@Injectable()
export class SharedDataService {
    qtype: QuestionType;
    courseName: string;
    answerArray: Answer[];

    getQuestionType() {
        return this.qtype;
    }

    setQuestionType(qtype: QuestionType) {
        this.qtype = qtype;
    }

    getCourseName() {
        return this.courseName;
    }

    setCourseName(name: string) {
        this.courseName = name;
    }

    getAnswers() {
        return this.answerArray;
    }

    setAnswers(array: Answer[]) {
        this.answerArray = array;
    }
}