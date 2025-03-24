import { StringEditor, DecimalEditor, DateEditor, BooleanEditor, PrefixedContext, initFormType } from "@serenity-is/corelib";

export interface PromotionsForm {
    Name: StringEditor;
    Description: StringEditor;
    DiscountPercentage: DecimalEditor;
    StartDate: DateEditor;
    EndDate: DateEditor;
    IsActive: BooleanEditor;
    CreatedAt: DateEditor;
    UpdatedAt: DateEditor;
}

export class PromotionsForm extends PrefixedContext {
    static readonly formKey = 'electroDB.Promotions';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!PromotionsForm.init)  {
            PromotionsForm.init = true;

            var w0 = StringEditor;
            var w1 = DecimalEditor;
            var w2 = DateEditor;
            var w3 = BooleanEditor;

            initFormType(PromotionsForm, [
                'Name', w0,
                'Description', w0,
                'DiscountPercentage', w1,
                'StartDate', w2,
                'EndDate', w2,
                'IsActive', w3,
                'CreatedAt', w2,
                'UpdatedAt', w2
            ]);
        }
    }
}