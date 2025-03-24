import { StringEditor, DecimalEditor, IntegerEditor, ServiceLookupEditor, DateEditor, PrefixedContext, initFormType } from "@serenity-is/corelib";

export interface ProductsForm {
    Name: StringEditor;
    Description: StringEditor;
    Price: DecimalEditor;
    StockQuantity: IntegerEditor;
    CategoryId: ServiceLookupEditor;
    CreatedAt: DateEditor;
    UpdatedAt: DateEditor;
    ImagePath: StringEditor;
}

export class ProductsForm extends PrefixedContext {
    static readonly formKey = 'Electro.Products';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!ProductsForm.init)  {
            ProductsForm.init = true;

            var w0 = StringEditor;
            var w1 = DecimalEditor;
            var w2 = IntegerEditor;
            var w3 = ServiceLookupEditor;
            var w4 = DateEditor;

            initFormType(ProductsForm, [
                'Name', w0,
                'Description', w0,
                'Price', w1,
                'StockQuantity', w2,
                'CategoryId', w3,
                'CreatedAt', w4,
                'UpdatedAt', w4,
                'ImagePath', w0
            ]);
        }
    }
}