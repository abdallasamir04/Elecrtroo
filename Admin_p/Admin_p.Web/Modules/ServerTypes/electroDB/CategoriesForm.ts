import { StringEditor, DateEditor, PrefixedContext, initFormType } from "@serenity-is/corelib";

export interface CategoriesForm {
    Name: StringEditor;
    Description: StringEditor;
    CreatedAt: DateEditor;
    UpdatedAt: DateEditor;
    ImageUrl: StringEditor;
}

export class CategoriesForm extends PrefixedContext {
    static readonly formKey = 'electroDB.Categories';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!CategoriesForm.init)  {
            CategoriesForm.init = true;

            var w0 = StringEditor;
            var w1 = DateEditor;

            initFormType(CategoriesForm, [
                'Name', w0,
                'Description', w0,
                'CreatedAt', w1,
                'UpdatedAt', w1,
                'ImageUrl', w0
            ]);
        }
    }
}