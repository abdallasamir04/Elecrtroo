import { ServiceLookupEditor, StringEditor, DateEditor, DecimalEditor, PrefixedContext, initFormType } from "@serenity-is/corelib";

export interface PaymentsForm {
    OrderId: ServiceLookupEditor;
    PaymentMethod: StringEditor;
    PaymentStatus: StringEditor;
    TransactionDate: DateEditor;
    Amount: DecimalEditor;
    CreatedAt: DateEditor;
    UpdatedAt: DateEditor;
    TransactionId: StringEditor;
}

export class PaymentsForm extends PrefixedContext {
    static readonly formKey = 'electroDB.Payments';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!PaymentsForm.init)  {
            PaymentsForm.init = true;

            var w0 = ServiceLookupEditor;
            var w1 = StringEditor;
            var w2 = DateEditor;
            var w3 = DecimalEditor;

            initFormType(PaymentsForm, [
                'OrderId', w0,
                'PaymentMethod', w1,
                'PaymentStatus', w1,
                'TransactionDate', w2,
                'Amount', w3,
                'CreatedAt', w2,
                'UpdatedAt', w2,
                'TransactionId', w1
            ]);
        }
    }
}