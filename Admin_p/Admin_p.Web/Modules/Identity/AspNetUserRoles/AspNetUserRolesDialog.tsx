import { Decorators, EntityDialog } from '@serenity-is/corelib';
import { AspNetUserRolesForm, AspNetUserRolesRow, AspNetUserRolesService } from '../../ServerTypes/Identity';

@Decorators.registerClass('Admin_p.Identity.AspNetUserRolesDialog')
export class AspNetUserRolesDialog extends EntityDialog<AspNetUserRolesRow, any> {
    protected getFormKey() { return AspNetUserRolesForm.formKey; }
    protected getRowDefinition() { return AspNetUserRolesRow; }
    protected getService() { return AspNetUserRolesService.baseUrl; }

    protected form = new AspNetUserRolesForm(this.idPrefix);
}