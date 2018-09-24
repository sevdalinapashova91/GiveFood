export class SingUpModel {
    constructor(
        public name: string,
        public email: string,
        public password: string,
        public confirmPassword: string,
        public type: string
    ) { }
}
