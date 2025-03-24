import { Decorators, EntityGrid } from '@serenity-is/corelib';
import { PromotionsColumns, PromotionsRow, PromotionsService } from '../../ServerTypes/electroDB';
import { PromotionsDialog } from './PromotionsDialog';

@Decorators.registerClass('Admin_p.electroDB.PromotionsGrid')
export class PromotionsGrid extends EntityGrid<PromotionsRow> {
    protected getColumnsKey() { return PromotionsColumns.columnsKey; }
    protected getDialogType() { return PromotionsDialog; }
    protected getRowDefinition() { return PromotionsRow; }
    protected getService() { return PromotionsService.baseUrl; }
}