import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { ShoppingCartsRow } from "./ShoppingCartsRow";

export interface ShoppingCartsColumns {
    CartId: Column<ShoppingCartsRow>;
    UserName: Column<ShoppingCartsRow>;
    ProductName: Column<ShoppingCartsRow>;
    Quantity: Column<ShoppingCartsRow>;
    CreatedAt: Column<ShoppingCartsRow>;
    UpdatedAt: Column<ShoppingCartsRow>;
}

export class ShoppingCartsColumns extends ColumnsBase<ShoppingCartsRow> {
    static readonly columnsKey = 'electroDB.ShoppingCarts';
    static readonly Fields = fieldsProxy<ShoppingCartsColumns>();
}