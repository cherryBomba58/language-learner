import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { QuestionType } from "../classes/questionType";

@Injectable()
export class QuestionTypeService {
    qtype: QuestionType;

    getQuestionType() {
        return this.qtype;
    }

    setQuestionType(qtype: QuestionType) {
        this.qtype = qtype;
    }
}