import { PrefixedContext, StringEditor, DateEditor, BooleanEditor, IntegerEditor, initFormType } from '@serenity-is/corelib';

export interface AspNetUsersForm {
    FirstName: StringEditor;
    CreatedAt: DateEditor;
    Role: StringEditor;
    ShippingAddress: StringEditor;
    PhoneNumber: StringEditor;
    UpdatedAt: DateEditor;
    UserName: StringEditor;
    NormalizedUserName: StringEditor;
    Email: StringEditor;
    NormalizedEmail: StringEditor;
    EmailConfirmed: BooleanEditor;
    PasswordHash: StringEditor;
    SecurityStamp: StringEditor;
    ConcurrencyStamp: StringEditor;
    PhoneNumberConfirmed: BooleanEditor;
    TwoFactorEnabled: BooleanEditor;
    LockoutEnd: StringEditor;
    LockoutEnabled: BooleanEditor;
    AccessFailedCount: IntegerEditor;
}

export class AspNetUsersForm extends PrefixedContext {
    static readonly formKey = 'Identity.AspNetUsers';
    private static init: boolean;
    
    constructor(prefix: string) {
        super(prefix);
        if (!AspNetUsersForm.init)  {
            AspNetUsersForm.init = true;
            
            var w0 = StringEditor;
            var w1 = DateEditor;
            var w2 = BooleanEditor;
            var w3 = IntegerEditor;

            initFormType(AspNetUsersForm, [
            'FirstName', w0,
            'CreatedAt', w1,
            'Role', w0,
            'ShippingAddress', w0,
            'PhoneNumber', w0,
            'UpdatedAt', w1,
            'UserName', w0,
            'NormalizedUserName', w0,
            'Email', w0,
            'NormalizedEmail', w0,
            'EmailConfirmed', w2,
            'PasswordHash', w0,
            'SecurityStamp', w0,
            'ConcurrencyStamp', w0,
            'PhoneNumberConfirmed', w2,
            'TwoFactorEnabled', w2,
            'LockoutEnd', w0,
            'LockoutEnabled', w2,
            'AccessFailedCount', w3,
            ]);
        }
    }
}