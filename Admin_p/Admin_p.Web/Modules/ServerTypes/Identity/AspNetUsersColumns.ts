import { ColumnsBase, fieldsProxy } from '@serenity-is/corelib';
import { Column } from '@serenity-is/sleekgrid';
import { AspNetUsersRow } from './AspNetUsersRow';

export interface AspNetUsersColumns {
    Id: Column<AspNetUsersRow>;
    FirstName: Column<AspNetUsersRow>;
    CreatedAt: Column<AspNetUsersRow>;
    Role: Column<AspNetUsersRow>;
    ShippingAddress: Column<AspNetUsersRow>;
    PhoneNumber: Column<AspNetUsersRow>;
    UpdatedAt: Column<AspNetUsersRow>;
    UserName: Column<AspNetUsersRow>;
    NormalizedUserName: Column<AspNetUsersRow>;
    Email: Column<AspNetUsersRow>;
    NormalizedEmail: Column<AspNetUsersRow>;
    EmailConfirmed: Column<AspNetUsersRow>;
    PasswordHash: Column<AspNetUsersRow>;
    SecurityStamp: Column<AspNetUsersRow>;
    ConcurrencyStamp: Column<AspNetUsersRow>;
    PhoneNumberConfirmed: Column<AspNetUsersRow>;
    TwoFactorEnabled: Column<AspNetUsersRow>;
    LockoutEnd: Column<AspNetUsersRow>;
    LockoutEnabled: Column<AspNetUsersRow>;
    AccessFailedCount: Column<AspNetUsersRow>;
}

export class AspNetUsersColumns extends ColumnsBase<AspNetUsersRow> {
    static readonly columnsKey = 'Identity.AspNetUsers';
    static readonly Fields = fieldsProxy<AspNetUsersColumns>();
}