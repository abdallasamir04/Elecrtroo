import { Decorators, EntityGrid } from '@serenity-is/corelib';
import { AspNetUserColumns, AspNetUserRow, AspNetUserService } from '../../ServerTypes/Identity';
import { AspNetUserDialog } from './AspNetUserDialog';

@Decorators.registerClass('Admin_p.Identity.AspNetUserGrid')
export class AspNetUserGrid extends EntityGrid<AspNetUserRow> {
    protected getColumnsKey() { return AspNetUserColumns.columnsKey; }
    protected getDialogType() { return AspNetUserDialog; }
    protected getRowDefinition() { return AspNetUserRow; }
    protected getService() { return AspNetUserService.baseUrl; }
}