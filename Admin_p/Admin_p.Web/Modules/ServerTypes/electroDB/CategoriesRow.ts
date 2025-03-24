import { fieldsProxy } from "@serenity-is/corelib";

export interface CategoriesRow {
    CategoryId?: number;
    Name?: string;
    Description?: string;
    CreatedAt?: string;
    UpdatedAt?: string;
    ImageUrl?: string;
}

export abstract class CategoriesRow {
    static readonly idProperty = 'CategoryId';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'electroDB.Categories';
    static readonly deletePermission = 'Categories';
    static readonly insertPermission = 'Categories';
    static readonly readPermission = 'Categories';
    static readonly updatePermission = 'Categories';

    static readonly Fields = fieldsProxy<CategoriesRow>();
}