import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { CourseService } from '../../services/course.service';
import { Course } from "../../classes/course";
import { QuestionsType } from "../../classes/questionsType";
import { QuestionTypesModel } from "../../classes/questionTypesModel";

@Component({
    selector: 'questions',
    templateUrl: './questions.component.html',
    styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit {
    courses: Observable<Course[]>;
    qtype: QuestionsType = QuestionsType.ToEnglish;

    qmodels: QuestionTypesModel[] = [
        new QuestionTypesModel(QuestionsType.ToEnglish, 'Magyarról angolra fordítás'),
        new QuestionTypesModel(QuestionsType.ToHungarian, 'Angolról magyarra fordítás'),
        new QuestionTypesModel(QuestionsType.ToImages, 'Képekhez szavak írása'),
        new QuestionTypesModel(QuestionsType.RightWord, 'Helyes szó kiválasztása egy listáról')
    ];

    constructor(private courseService: CourseService) { }

    ngOnInit() {
        this.getCourses();
    }

    getCourses() {
        this.courses = this.courseService.getCourses();
    }

    startQuestions(courseID: number) {
        console.log(courseID, this.qtype, this.qmodels);
    }
}