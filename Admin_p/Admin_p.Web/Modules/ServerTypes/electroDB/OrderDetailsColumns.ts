import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { OrderDetailsRow } from "./OrderDetailsRow";

export interface OrderDetailsColumns {
    OrderDetailId: Column<OrderDetailsRow>;
    OrderStatus: Column<OrderDetailsRow>;
    ProductName: Column<OrderDetailsRow>;
    Quantity: Column<OrderDetailsRow>;
    UnitPrice: Column<OrderDetailsRow>;
    Subtotal: Column<OrderDetailsRow>;
    CreatedAt: Column<OrderDetailsRow>;
    UpdatedAt: Column<OrderDetailsRow>;
}

export class OrderDetailsColumns extends ColumnsBase<OrderDetailsRow> {
    static readonly columnsKey = 'electroDB.OrderDetails';
    static readonly Fields = fieldsProxy<OrderDetailsColumns>();
}