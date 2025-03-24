import { ServiceLookupEditor, DateEditor, PrefixedContext, initFormType } from "@serenity-is/corelib";

export interface PromotionProductsForm {
    PromotionId: ServiceLookupEditor;
    ProductId: ServiceLookupEditor;
    CreatedAt: DateEditor;
    UpdatedAt: DateEditor;
}

export class PromotionProductsForm extends PrefixedContext {
    static readonly formKey = 'electroDB.PromotionProducts';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!PromotionProductsForm.init)  {
            PromotionProductsForm.init = true;

            var w0 = ServiceLookupEditor;
            var w1 = DateEditor;

            initFormType(PromotionProductsForm, [
                'PromotionId', w0,
                'ProductId', w0,
                'CreatedAt', w1,
                'UpdatedAt', w1
            ]);
        }
    }
}