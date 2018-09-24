export class UserModel {
constructor(
    public name: string,
    public email: string,
    public description: string,
    public type: number,
    public status?: number,
    public document?: DocumentModel ) {
    }
}

export class DocumentModel {
    constructor(
        public name: string,
        public id: number
    ) { }
}

export class ProfileModel {
  constructor(
    public name: string,
    public email: string,
    public description: string,
    public type: number,
    public documentName?: string) {
    }
}
