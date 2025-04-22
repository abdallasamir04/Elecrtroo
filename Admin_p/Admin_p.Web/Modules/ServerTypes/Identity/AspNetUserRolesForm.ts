import { PrefixedContext, StringEditor, initFormType } from '@serenity-is/corelib';

export interface AspNetUserRolesForm {
    RoleId: StringEditor;
}

export class AspNetUserRolesForm extends PrefixedContext {
    static readonly formKey = 'Identity.AspNetUserRoles';
    private static init: boolean;
    
    constructor(prefix: string) {
        super(prefix);
        if (!AspNetUserRolesForm.init)  {
            AspNetUserRolesForm.init = true;
            
            var w0 = StringEditor;

            initFormType(AspNetUserRolesForm, [
            'RoleId', w0,
            ]);
        }
    }
}