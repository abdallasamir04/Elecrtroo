import { ColumnsBase, fieldsProxy } from '@serenity-is/corelib';
import { Column } from '@serenity-is/sleekgrid';
import { AspNetUserRow } from './AspNetUserRow';

export interface AspNetUserColumns {
    Id: Column<AspNetUserRow>;
    Name: Column<AspNetUserRow>;
    NormalizedName: Column<AspNetUserRow>;
    ConcurrencyStamp: Column<AspNetUserRow>;
}

export class AspNetUserColumns extends ColumnsBase<AspNetUserRow> {
    static readonly columnsKey = 'Identity.AspNetUser';
    static readonly Fields = fieldsProxy<AspNetUserColumns>();
}