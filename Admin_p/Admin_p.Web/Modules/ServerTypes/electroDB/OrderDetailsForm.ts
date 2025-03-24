import { ServiceLookupEditor, IntegerEditor, DecimalEditor, DateEditor, PrefixedContext, initFormType } from "@serenity-is/corelib";

export interface OrderDetailsForm {
    OrderId: ServiceLookupEditor;
    ProductId: ServiceLookupEditor;
    Quantity: IntegerEditor;
    UnitPrice: DecimalEditor;
    Subtotal: DecimalEditor;
    CreatedAt: DateEditor;
    UpdatedAt: DateEditor;
}

export class OrderDetailsForm extends PrefixedContext {
    static readonly formKey = 'electroDB.OrderDetails';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!OrderDetailsForm.init)  {
            OrderDetailsForm.init = true;

            var w0 = ServiceLookupEditor;
            var w1 = IntegerEditor;
            var w2 = DecimalEditor;
            var w3 = DateEditor;

            initFormType(OrderDetailsForm, [
                'OrderId', w0,
                'ProductId', w0,
                'Quantity', w1,
                'UnitPrice', w2,
                'Subtotal', w2,
                'CreatedAt', w3,
                'UpdatedAt', w3
            ]);
        }
    }
}