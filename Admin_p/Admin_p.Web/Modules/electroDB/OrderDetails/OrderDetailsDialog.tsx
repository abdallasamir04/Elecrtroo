import { Decorators, EntityDialog } from '@serenity-is/corelib';
import { OrderDetailsForm, OrderDetailsRow, OrderDetailsService } from '../../ServerTypes/electroDB';

@Decorators.registerClass('Admin_p.electroDB.OrderDetailsDialog')
export class OrderDetailsDialog extends EntityDialog<OrderDetailsRow, any> {
    protected getFormKey() { return OrderDetailsForm.formKey; }
    protected getRowDefinition() { return OrderDetailsRow; }
    protected getService() { return OrderDetailsService.baseUrl; }

    protected form = new OrderDetailsForm(this.idPrefix);
}