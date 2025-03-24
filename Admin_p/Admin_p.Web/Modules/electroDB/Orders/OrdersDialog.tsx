import { Decorators, EntityDialog } from '@serenity-is/corelib';
import { OrdersForm, OrdersRow, OrdersService } from '../../ServerTypes/electroDB';

@Decorators.registerClass('Admin_p.electroDB.OrdersDialog')
export class OrdersDialog extends EntityDialog<OrdersRow, any> {
    protected getFormKey() { return OrdersForm.formKey; }
    protected getRowDefinition() { return OrdersRow; }
    protected getService() { return OrdersService.baseUrl; }

    protected form = new OrdersForm(this.idPrefix);
}