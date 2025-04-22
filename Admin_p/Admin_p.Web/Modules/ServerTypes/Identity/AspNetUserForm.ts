import { PrefixedContext, StringEditor, initFormType } from '@serenity-is/corelib';

export interface AspNetUserForm {
    Name: StringEditor;
    NormalizedName: StringEditor;
    ConcurrencyStamp: StringEditor;
}

export class AspNetUserForm extends PrefixedContext {
    static readonly formKey = 'Identity.AspNetUser';
    private static init: boolean;
    
    constructor(prefix: string) {
        super(prefix);
        if (!AspNetUserForm.init)  {
            AspNetUserForm.init = true;
            
            var w0 = StringEditor;

            initFormType(AspNetUserForm, [
            'Name', w0,
            'NormalizedName', w0,
            'ConcurrencyStamp', w0,
            ]);
        }
    }
}