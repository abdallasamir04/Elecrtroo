import { fieldsProxy } from "@serenity-is/corelib";

export interface ProductsRow {
    ProductId?: number;
    Name?: string;
    Description?: string;
    Price?: number;
    StockQuantity?: number;
    CategoryId?: number;
    CreatedAt?: string;
    UpdatedAt?: string;
    ImagePath?: string;
    CategoryName?: string;
}

export abstract class ProductsRow {
    static readonly idProperty = 'ProductId';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'Electro.Products';
    static readonly deletePermission = 'Products';
    static readonly insertPermission = 'Products';
    static readonly readPermission = 'Products';
    static readonly updatePermission = 'Products';

    static readonly Fields = fieldsProxy<ProductsRow>();
}