import { Decorators, EntityDialog } from '@serenity-is/corelib';
import { UsersForm, UsersRow, UsersService } from '../../ServerTypes/electroDB';

@Decorators.registerClass('Admin_p.electroDB.UsersDialog')
export class UsersDialog extends EntityDialog<UsersRow, any> {
    protected getFormKey() { return UsersForm.formKey; }
    protected getRowDefinition() { return UsersRow; }
    protected getService() { return UsersService.baseUrl; }

    protected form = new UsersForm(this.idPrefix);
}