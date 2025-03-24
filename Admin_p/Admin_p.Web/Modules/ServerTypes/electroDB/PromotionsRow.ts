import { fieldsProxy } from "@serenity-is/corelib";

export interface PromotionsRow {
    PromotionId?: number;
    Name?: string;
    Description?: string;
    DiscountPercentage?: number;
    StartDate?: string;
    EndDate?: string;
    IsActive?: boolean;
    CreatedAt?: string;
    UpdatedAt?: string;
}

export abstract class PromotionsRow {
    static readonly idProperty = 'PromotionId';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'electroDB.Promotions';
    static readonly deletePermission = 'Promotions';
    static readonly insertPermission = 'Promotions';
    static readonly readPermission = 'Promotions';
    static readonly updatePermission = 'Promotions';

    static readonly Fields = fieldsProxy<PromotionsRow>();
}