import { ColumnsBase, fieldsProxy } from '@serenity-is/corelib';
import { Column } from '@serenity-is/sleekgrid';
import { AspNetUserRolesRow } from './AspNetUserRolesRow';

export interface AspNetUserRolesColumns {
    UserId: Column<AspNetUserRolesRow>;
    RoleId: Column<AspNetUserRolesRow>;
}

export class AspNetUserRolesColumns extends ColumnsBase<AspNetUserRolesRow> {
    static readonly columnsKey = 'Identity.AspNetUserRoles';
    static readonly Fields = fieldsProxy<AspNetUserRolesColumns>();
}