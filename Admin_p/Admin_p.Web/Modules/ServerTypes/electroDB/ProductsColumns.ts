import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { ProductsRow } from "./ProductsRow";

export interface ProductsColumns {
    ProductId: Column<ProductsRow>;
    Name: Column<ProductsRow>;
    Description: Column<ProductsRow>;
    Price: Column<ProductsRow>;
    StockQuantity: Column<ProductsRow>;
    CategoryName: Column<ProductsRow>;
    CreatedAt: Column<ProductsRow>;
    UpdatedAt: Column<ProductsRow>;
    ImagePath: Column<ProductsRow>;
}

export class ProductsColumns extends ColumnsBase<ProductsRow> {
    static readonly columnsKey = 'electroDB.Products';
    static readonly Fields = fieldsProxy<ProductsColumns>();
}