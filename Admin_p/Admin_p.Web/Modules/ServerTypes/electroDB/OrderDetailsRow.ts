import { fieldsProxy } from "@serenity-is/corelib";

export interface OrderDetailsRow {
    OrderDetailId?: number;
    OrderId?: number;
    ProductId?: number;
    Quantity?: number;
    UnitPrice?: number;
    Subtotal?: number;
    CreatedAt?: string;
    UpdatedAt?: string;
    OrderStatus?: string;
    ProductName?: string;
}

export abstract class OrderDetailsRow {
    static readonly idProperty = 'OrderDetailId';
    static readonly localTextPrefix = 'electroDB.OrderDetails';
    static readonly deletePermission = 'OrderDetails';
    static readonly insertPermission = 'OrderDetails';
    static readonly readPermission = 'OrderDetails';
    static readonly updatePermission = 'OrderDetails';

    static readonly Fields = fieldsProxy<OrderDetailsRow>();
}