import { fieldsProxy } from "@serenity-is/corelib";

export interface PaymentsRow {
    PaymentId?: number;
    OrderId?: number;
    PaymentMethod?: string;
    PaymentStatus?: string;
    TransactionDate?: string;
    Amount?: number;
    CreatedAt?: string;
    UpdatedAt?: string;
    TransactionId?: string;
    OrderStatus?: string;
}

export abstract class PaymentsRow {
    static readonly idProperty = 'PaymentId';
    static readonly nameProperty = 'PaymentMethod';
    static readonly localTextPrefix = 'electroDB.Payments';
    static readonly deletePermission = 'Payments';
    static readonly insertPermission = 'Payments';
    static readonly readPermission = 'Payments';
    static readonly updatePermission = 'Payments';

    static readonly Fields = fieldsProxy<PaymentsRow>();
}