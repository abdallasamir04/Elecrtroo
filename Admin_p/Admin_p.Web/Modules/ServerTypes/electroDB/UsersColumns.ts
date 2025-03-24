import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { UsersRow } from "./UsersRow";

export interface UsersColumns {
    UserId: Column<UsersRow>;
    UserName: Column<UsersRow>;
    Email: Column<UsersRow>;
    PasswordHash: Column<UsersRow>;
    Role: Column<UsersRow>;
    CreatedAt: Column<UsersRow>;
    UpdatedAt: Column<UsersRow>;
    ShippingAddress: Column<UsersRow>;
    PhoneNumber: Column<UsersRow>;
}

export class UsersColumns extends ColumnsBase<UsersRow> {
    static readonly columnsKey = 'electroDB.Users';
    static readonly Fields = fieldsProxy<UsersColumns>();
}