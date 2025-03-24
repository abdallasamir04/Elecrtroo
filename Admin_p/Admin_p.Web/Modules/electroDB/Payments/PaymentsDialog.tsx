import { Decorators, EntityDialog } from '@serenity-is/corelib';
import { PaymentsForm, PaymentsRow, PaymentsService } from '../../ServerTypes/electroDB';

@Decorators.registerClass('Admin_p.electroDB.PaymentsDialog')
export class PaymentsDialog extends EntityDialog<PaymentsRow, any> {
    protected getFormKey() { return PaymentsForm.formKey; }
    protected getRowDefinition() { return PaymentsRow; }
    protected getService() { return PaymentsService.baseUrl; }

    protected form = new PaymentsForm(this.idPrefix);
}