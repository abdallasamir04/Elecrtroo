import { Decorators, EntityGrid } from '@serenity-is/corelib';
import { AspNetUserRolesColumns, AspNetUserRolesRow, AspNetUserRolesService } from '../../ServerTypes/Identity';
import { AspNetUserRolesDialog } from './AspNetUserRolesDialog';

@Decorators.registerClass('Admin_p.Identity.AspNetUserRolesGrid')
export class AspNetUserRolesGrid extends EntityGrid<AspNetUserRolesRow> {
    protected getColumnsKey() { return AspNetUserRolesColumns.columnsKey; }
    protected getDialogType() { return AspNetUserRolesDialog; }
    protected getRowDefinition() { return AspNetUserRolesRow; }
    protected getService() { return AspNetUserRolesService.baseUrl; }
}