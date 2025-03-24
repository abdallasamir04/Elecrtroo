import { Decorators, EntityDialog } from '@serenity-is/corelib';
import { PromotionProductsForm, PromotionProductsRow, PromotionProductsService } from '../../ServerTypes/electroDB';

@Decorators.registerClass('Admin_p.electroDB.PromotionProductsDialog')
export class PromotionProductsDialog extends EntityDialog<PromotionProductsRow, any> {
    protected getFormKey() { return PromotionProductsForm.formKey; }
    protected getRowDefinition() { return PromotionProductsRow; }
    protected getService() { return PromotionProductsService.baseUrl; }

    protected form = new PromotionProductsForm(this.idPrefix);
}