import { fieldsProxy } from '@serenity-is/corelib';

export interface AspNetUserRow {
    Id?: string;
    Name?: string;
    NormalizedName?: string;
    ConcurrencyStamp?: string;
}

export abstract class AspNetUserRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Id';
    static readonly localTextPrefix = 'Identity.AspNetUser';

    static readonly deletePermission = 'Identity:Role';
    static readonly insertPermission = 'Identity:Role';
    static readonly readPermission = 'Identity:Role';
    static readonly updatePermission = 'Identity:Role';

    static readonly Fields = fieldsProxy<AspNetUserRow>();
}