import { ServiceLookupEditor, DateEditor, DecimalEditor, StringEditor, PrefixedContext, initFormType } from "@serenity-is/corelib";

export interface OrdersForm {
    UserId: ServiceLookupEditor;
    OrderDate: DateEditor;
    TotalAmount: DecimalEditor;
    Status: StringEditor;
    CreatedAt: DateEditor;
    UpdatedAt: DateEditor;
}

export class OrdersForm extends PrefixedContext {
    static readonly formKey = 'electroDB.Orders';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!OrdersForm.init)  {
            OrdersForm.init = true;

            var w0 = ServiceLookupEditor;
            var w1 = DateEditor;
            var w2 = DecimalEditor;
            var w3 = StringEditor;

            initFormType(OrdersForm, [
                'UserId', w0,
                'OrderDate', w1,
                'TotalAmount', w2,
                'Status', w3,
                'CreatedAt', w1,
                'UpdatedAt', w1
            ]);
        }
    }
}