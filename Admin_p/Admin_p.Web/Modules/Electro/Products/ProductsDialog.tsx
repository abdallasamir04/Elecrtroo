import { Decorators, EntityDialog } from '@serenity-is/corelib';
import { ProductsForm, ProductsRow, ProductsService } from '../../ServerTypes/Electro';

@Decorators.registerClass('Admin_p.Electro.ProductsDialog')
export class ProductsDialog extends EntityDialog<ProductsRow, any> {
    protected getFormKey() { return ProductsForm.formKey; }
    protected getRowDefinition() { return ProductsRow; }
    protected getService() { return ProductsService.baseUrl; }

    protected form = new ProductsForm(this.idPrefix);
}