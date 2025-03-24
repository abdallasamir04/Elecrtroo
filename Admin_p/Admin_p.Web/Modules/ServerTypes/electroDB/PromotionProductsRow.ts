import { fieldsProxy } from "@serenity-is/corelib";

export interface PromotionProductsRow {
    PromotionProductId?: number;
    PromotionId?: number;
    ProductId?: number;
    CreatedAt?: string;
    UpdatedAt?: string;
    PromotionName?: string;
    ProductName?: string;
}

export abstract class PromotionProductsRow {
    static readonly idProperty = 'PromotionProductId';
    static readonly localTextPrefix = 'electroDB.PromotionProducts';
    static readonly deletePermission = 'PromotionProducts';
    static readonly insertPermission = 'PromotionProducts';
    static readonly readPermission = 'PromotionProducts';
    static readonly updatePermission = 'PromotionProducts';

    static readonly Fields = fieldsProxy<PromotionProductsRow>();
}