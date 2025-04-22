import { fieldsProxy } from '@serenity-is/corelib';

export interface AspNetUsersRow {
    Id?: string;
    FirstName?: string;
    CreatedAt?: string;
    Role?: string;
    ShippingAddress?: string;
    PhoneNumber?: string;
    UpdatedAt?: string;
    UserName?: string;
    NormalizedUserName?: string;
    Email?: string;
    NormalizedEmail?: string;
    EmailConfirmed?: boolean;
    PasswordHash?: string;
    SecurityStamp?: string;
    ConcurrencyStamp?: string;
    PhoneNumberConfirmed?: boolean;
    TwoFactorEnabled?: boolean;
    LockoutEnd?: any;
    LockoutEnabled?: boolean;
    AccessFailedCount?: number;
}

export abstract class AspNetUsersRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Id';
    static readonly localTextPrefix = 'Identity.AspNetUsers';

    static readonly deletePermission = 'Identity:User';
    static readonly insertPermission = 'Identity:User';
    static readonly readPermission = 'Identity:User';
    static readonly updatePermission = 'Identity:User';

    static readonly Fields = fieldsProxy<AspNetUsersRow>();
}