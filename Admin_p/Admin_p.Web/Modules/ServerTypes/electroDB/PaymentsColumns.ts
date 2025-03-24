import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { PaymentsRow } from "./PaymentsRow";

export interface PaymentsColumns {
    PaymentId: Column<PaymentsRow>;
    OrderStatus: Column<PaymentsRow>;
    PaymentMethod: Column<PaymentsRow>;
    PaymentStatus: Column<PaymentsRow>;
    TransactionDate: Column<PaymentsRow>;
    Amount: Column<PaymentsRow>;
    CreatedAt: Column<PaymentsRow>;
    UpdatedAt: Column<PaymentsRow>;
    TransactionId: Column<PaymentsRow>;
}

export class PaymentsColumns extends ColumnsBase<PaymentsRow> {
    static readonly columnsKey = 'electroDB.Payments';
    static readonly Fields = fieldsProxy<PaymentsColumns>();
}