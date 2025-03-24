import { Decorators, EntityGrid } from '@serenity-is/corelib';
import { OrdersColumns, OrdersRow, OrdersService } from '../../ServerTypes/electroDB';
import { OrdersDialog } from './OrdersDialog';

@Decorators.registerClass('Admin_p.electroDB.OrdersGrid')
export class OrdersGrid extends EntityGrid<OrdersRow> {
    protected getColumnsKey() { return OrdersColumns.columnsKey; }
    protected getDialogType() { return OrdersDialog; }
    protected getRowDefinition() { return OrdersRow; }
    protected getService() { return OrdersService.baseUrl; }
}