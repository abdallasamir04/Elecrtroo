import { ServiceLookupEditor, IntegerEditor, DateEditor, PrefixedContext, initFormType } from "@serenity-is/corelib";

export interface ShoppingCartsForm {
    UserId: ServiceLookupEditor;
    ProductId: ServiceLookupEditor;
    Quantity: IntegerEditor;
    CreatedAt: DateEditor;
    UpdatedAt: DateEditor;
}

export class ShoppingCartsForm extends PrefixedContext {
    static readonly formKey = 'electroDB.ShoppingCarts';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!ShoppingCartsForm.init)  {
            ShoppingCartsForm.init = true;

            var w0 = ServiceLookupEditor;
            var w1 = IntegerEditor;
            var w2 = DateEditor;

            initFormType(ShoppingCartsForm, [
                'UserId', w0,
                'ProductId', w0,
                'Quantity', w1,
                'CreatedAt', w2,
                'UpdatedAt', w2
            ]);
        }
    }
}