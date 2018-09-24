export class NotificationModel {
constructor(
    public id: number,
    public isRead: boolean,
    public messageType: number,
    public dateCreated: Date,
    public senderName: string) {
    }
}
export class NotificationCountModel {
constructor(
    public userId: string,
    public notificationCount: number) {
    }
}
