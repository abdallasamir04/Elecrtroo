import { fieldsProxy } from "@serenity-is/corelib";

export interface UsersRow {
    UserId?: number;
    UserName?: string;
    Email?: string;
    PasswordHash?: string;
    Role?: string;
    CreatedAt?: string;
    UpdatedAt?: string;
    ShippingAddress?: string;
    PhoneNumber?: string;
}

export abstract class UsersRow {
    static readonly idProperty = 'UserId';
    static readonly nameProperty = 'UserName';
    static readonly localTextPrefix = 'electroDB.Users';
    static readonly deletePermission = 'Users';
    static readonly insertPermission = 'Users';
    static readonly readPermission = 'Users';
    static readonly updatePermission = 'Users';

    static readonly Fields = fieldsProxy<UsersRow>();
}