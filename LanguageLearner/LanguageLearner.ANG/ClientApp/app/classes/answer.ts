export class Answer {
    constructor(
        public learnableID: number,
        public originalWord: string,
        public goodAnswer: string,
        public answer: string = "",
        public right: boolean = false
    ) { }
}