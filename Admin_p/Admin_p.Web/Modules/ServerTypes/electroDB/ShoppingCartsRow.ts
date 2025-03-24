import { fieldsProxy } from "@serenity-is/corelib";

export interface ShoppingCartsRow {
    CartId?: number;
    UserId?: number;
    ProductId?: number;
    Quantity?: number;
    CreatedAt?: string;
    UpdatedAt?: string;
    UserName?: string;
    ProductName?: string;
}

export abstract class ShoppingCartsRow {
    static readonly idProperty = 'CartId';
    static readonly localTextPrefix = 'electroDB.ShoppingCarts';
    static readonly deletePermission = 'ShoppingCarts';
    static readonly insertPermission = 'ShoppingCarts';
    static readonly readPermission = 'ShoppingCarts';
    static readonly updatePermission = 'ShoppingCarts';

    static readonly Fields = fieldsProxy<ShoppingCartsRow>();
}