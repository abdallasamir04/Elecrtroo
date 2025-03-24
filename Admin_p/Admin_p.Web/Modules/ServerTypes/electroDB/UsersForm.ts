import { StringEditor, DateEditor, PrefixedContext, initFormType } from "@serenity-is/corelib";

export interface UsersForm {
    UserName: StringEditor;
    Email: StringEditor;
    PasswordHash: StringEditor;
    Role: StringEditor;
    CreatedAt: DateEditor;
    UpdatedAt: DateEditor;
    ShippingAddress: StringEditor;
    PhoneNumber: StringEditor;
}

export class UsersForm extends PrefixedContext {
    static readonly formKey = 'electroDB.Users';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!UsersForm.init)  {
            UsersForm.init = true;

            var w0 = StringEditor;
            var w1 = DateEditor;

            initFormType(UsersForm, [
                'UserName', w0,
                'Email', w0,
                'PasswordHash', w0,
                'Role', w0,
                'CreatedAt', w1,
                'UpdatedAt', w1,
                'ShippingAddress', w0,
                'PhoneNumber', w0
            ]);
        }
    }
}