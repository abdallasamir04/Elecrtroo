import { Decorators, EntityDialog } from '@serenity-is/corelib';
import { PromotionsForm, PromotionsRow, PromotionsService } from '../../ServerTypes/electroDB';

@Decorators.registerClass('Admin_p.electroDB.PromotionsDialog')
export class PromotionsDialog extends EntityDialog<PromotionsRow, any> {
    protected getFormKey() { return PromotionsForm.formKey; }
    protected getRowDefinition() { return PromotionsRow; }
    protected getService() { return PromotionsService.baseUrl; }

    protected form = new PromotionsForm(this.idPrefix);
}