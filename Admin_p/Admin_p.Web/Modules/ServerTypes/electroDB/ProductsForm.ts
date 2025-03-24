import { StringEditor, DecimalEditor, IntegerEditor, DateEditor, PrefixedContext, initFormType } from "@serenity-is/corelib";

export interface ProductsForm {
    Name: StringEditor;
    Description: StringEditor;
    Price: DecimalEditor;
    StockQuantity: IntegerEditor;
    CategoryId: IntegerEditor;
    CreatedAt: DateEditor;
    UpdatedAt: DateEditor;
    ImagePath: StringEditor;
}

export class ProductsForm extends PrefixedContext {
    static readonly formKey = 'electroDB.Products';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!ProductsForm.init)  {
            ProductsForm.init = true;

            var w0 = StringEditor;
            var w1 = DecimalEditor;
            var w2 = IntegerEditor;
            var w3 = DateEditor;

            initFormType(ProductsForm, [
                'Name', w0,
                'Description', w0,
                'Price', w1,
                'StockQuantity', w2,
                'CategoryId', w2,
                'CreatedAt', w3,
                'UpdatedAt', w3,
                'ImagePath', w0
            ]);
        }
    }
}