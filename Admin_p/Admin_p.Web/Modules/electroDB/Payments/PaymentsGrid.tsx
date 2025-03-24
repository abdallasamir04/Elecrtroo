import { Decorators, EntityGrid } from '@serenity-is/corelib';
import { PaymentsColumns, PaymentsRow, PaymentsService } from '../../ServerTypes/electroDB';
import { PaymentsDialog } from './PaymentsDialog';

@Decorators.registerClass('Admin_p.electroDB.PaymentsGrid')
export class PaymentsGrid extends EntityGrid<PaymentsRow> {
    protected getColumnsKey() { return PaymentsColumns.columnsKey; }
    protected getDialogType() { return PaymentsDialog; }
    protected getRowDefinition() { return PaymentsRow; }
    protected getService() { return PaymentsService.baseUrl; }
}