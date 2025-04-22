import { Decorators, EntityGrid } from '@serenity-is/corelib';
import { AspNetUsersColumns, AspNetUsersRow, AspNetUsersService } from '../../ServerTypes/Identity';
import { AspNetUsersDialog } from './AspNetUsersDialog';

@Decorators.registerClass('Admin_p.Identity.AspNetUsersGrid')
export class AspNetUsersGrid extends EntityGrid<AspNetUsersRow> {
    protected getColumnsKey() { return AspNetUsersColumns.columnsKey; }
    protected getDialogType() { return AspNetUsersDialog; }
    protected getRowDefinition() { return AspNetUsersRow; }
    protected getService() { return AspNetUsersService.baseUrl; }
}