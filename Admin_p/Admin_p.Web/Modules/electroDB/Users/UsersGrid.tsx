import { Decorators, EntityGrid } from '@serenity-is/corelib';
import { UsersColumns, UsersRow, UsersService } from '../../ServerTypes/electroDB';
import { UsersDialog } from './UsersDialog';

@Decorators.registerClass('Admin_p.electroDB.UsersGrid')
export class UsersGrid extends EntityGrid<UsersRow> {
    protected getColumnsKey() { return UsersColumns.columnsKey; }
    protected getDialogType() { return UsersDialog; }
    protected getRowDefinition() { return UsersRow; }
    protected getService() { return UsersService.baseUrl; }
}