import { Decorators, EntityDialog } from '@serenity-is/corelib';
import { CategoriesForm, CategoriesRow, CategoriesService } from '../../ServerTypes/electroDB';

@Decorators.registerClass('Admin_p.electroDB.CategoriesDialog')
export class CategoriesDialog extends EntityDialog<CategoriesRow, any> {
    protected getFormKey() { return CategoriesForm.formKey; }
    protected getRowDefinition() { return CategoriesRow; }
    protected getService() { return CategoriesService.baseUrl; }

    protected form = new CategoriesForm(this.idPrefix);
}