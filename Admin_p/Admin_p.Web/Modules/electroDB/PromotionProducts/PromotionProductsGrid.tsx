import { Decorators, EntityGrid } from '@serenity-is/corelib';
import { PromotionProductsColumns, PromotionProductsRow, PromotionProductsService } from '../../ServerTypes/electroDB';
import { PromotionProductsDialog } from './PromotionProductsDialog';

@Decorators.registerClass('Admin_p.electroDB.PromotionProductsGrid')
export class PromotionProductsGrid extends EntityGrid<PromotionProductsRow> {
    protected getColumnsKey() { return PromotionProductsColumns.columnsKey; }
    protected getDialogType() { return PromotionProductsDialog; }
    protected getRowDefinition() { return PromotionProductsRow; }
    protected getService() { return PromotionProductsService.baseUrl; }
}