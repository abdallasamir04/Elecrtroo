import { Decorators, EntityGrid } from '@serenity-is/corelib';
import { ShoppingCartsColumns, ShoppingCartsRow, ShoppingCartsService } from '../../ServerTypes/electroDB';
import { ShoppingCartsDialog } from './ShoppingCartsDialog';

@Decorators.registerClass('Admin_p.electroDB.ShoppingCartsGrid')
export class ShoppingCartsGrid extends EntityGrid<ShoppingCartsRow> {
    protected getColumnsKey() { return ShoppingCartsColumns.columnsKey; }
    protected getDialogType() { return ShoppingCartsDialog; }
    protected getRowDefinition() { return ShoppingCartsRow; }
    protected getService() { return ShoppingCartsService.baseUrl; }
}