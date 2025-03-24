import { Decorators, EntityDialog } from '@serenity-is/corelib';
import { ShoppingCartsForm, ShoppingCartsRow, ShoppingCartsService } from '../../ServerTypes/electroDB';

@Decorators.registerClass('Admin_p.electroDB.ShoppingCartsDialog')
export class ShoppingCartsDialog extends EntityDialog<ShoppingCartsRow, any> {
    protected getFormKey() { return ShoppingCartsForm.formKey; }
    protected getRowDefinition() { return ShoppingCartsRow; }
    protected getService() { return ShoppingCartsService.baseUrl; }

    protected form = new ShoppingCartsForm(this.idPrefix);
}