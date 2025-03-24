import { Decorators, EntityGrid } from '@serenity-is/corelib';
import { OrderDetailsColumns, OrderDetailsRow, OrderDetailsService } from '../../ServerTypes/electroDB';
import { OrderDetailsDialog } from './OrderDetailsDialog';

@Decorators.registerClass('Admin_p.electroDB.OrderDetailsGrid')
export class OrderDetailsGrid extends EntityGrid<OrderDetailsRow> {
    protected getColumnsKey() { return OrderDetailsColumns.columnsKey; }
    protected getDialogType() { return OrderDetailsDialog; }
    protected getRowDefinition() { return OrderDetailsRow; }
    protected getService() { return OrderDetailsService.baseUrl; }
}