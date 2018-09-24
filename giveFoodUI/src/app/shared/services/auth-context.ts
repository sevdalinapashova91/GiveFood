import { SimpleClaim } from './simple.claims.model';
import { UserModel } from './user.model';

export class AuthContext {
    userProfile: UserModel;
    claims: SimpleClaim[];
}
