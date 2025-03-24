import { Decorators, EntityGrid } from '@serenity-is/corelib';
import { ProductsColumns, ProductsRow, ProductsService } from '../../ServerTypes/Electro';
import { ProductsDialog } from './ProductsDialog';

@Decorators.registerClass('Admin_p.Electro.ProductsGrid')
export class ProductsGrid extends EntityGrid<ProductsRow> {
    protected getColumnsKey() { return ProductsColumns.columnsKey; }
    protected getDialogType() { return ProductsDialog; }
    protected getRowDefinition() { return ProductsRow; }
    protected getService() { return ProductsService.baseUrl; }
}