import { Decorators, EntityDialog } from '@serenity-is/corelib';
import { AspNetUserForm, AspNetUserRow, AspNetUserService } from '../../ServerTypes/Identity';

@Decorators.registerClass('Admin_p.Identity.AspNetUserDialog')
export class AspNetUserDialog extends EntityDialog<AspNetUserRow, any> {
    protected getFormKey() { return AspNetUserForm.formKey; }
    protected getRowDefinition() { return AspNetUserRow; }
    protected getService() { return AspNetUserService.baseUrl; }

    protected form = new AspNetUserForm(this.idPrefix);
}