import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { OrdersRow } from "./OrdersRow";

export interface OrdersColumns {
    OrderId: Column<OrdersRow>;
    UserName: Column<OrdersRow>;
    OrderDate: Column<OrdersRow>;
    TotalAmount: Column<OrdersRow>;
    Status: Column<OrdersRow>;
    CreatedAt: Column<OrdersRow>;
    UpdatedAt: Column<OrdersRow>;
}

export class OrdersColumns extends ColumnsBase<OrdersRow> {
    static readonly columnsKey = 'electroDB.Orders';
    static readonly Fields = fieldsProxy<OrdersColumns>();
}