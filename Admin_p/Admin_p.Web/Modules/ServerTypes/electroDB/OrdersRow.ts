import { fieldsProxy } from "@serenity-is/corelib";

export interface OrdersRow {
    OrderId?: number;
    UserId?: number;
    OrderDate?: string;
    TotalAmount?: number;
    Status?: string;
    CreatedAt?: string;
    UpdatedAt?: string;
    UserName?: string;
}

export abstract class OrdersRow {
    static readonly idProperty = 'OrderId';
    static readonly nameProperty = 'Status';
    static readonly localTextPrefix = 'electroDB.Orders';
    static readonly deletePermission = 'Orders';
    static readonly insertPermission = 'Orders';
    static readonly readPermission = 'Orders';
    static readonly updatePermission = 'Orders';

    static readonly Fields = fieldsProxy<OrdersRow>();
}