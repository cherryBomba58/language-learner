import { QuestionType } from "./questionType";

export class QuestionTypesModel {
    constructor(
        public id: QuestionType,
        public text: string
    ) { }
}