export class Answer {
    constructor(
        public learnableID: number,
        public goodAnswer: string,
        public answer: string = "",
        public right: boolean = false
    ) { }
}