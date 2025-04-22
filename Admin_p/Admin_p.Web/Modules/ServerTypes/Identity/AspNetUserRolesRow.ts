import { fieldsProxy } from '@serenity-is/corelib';

export interface AspNetUserRolesRow {
    UserId?: string;
    RoleId?: string;
    UserFirstName?: string;
    RoleName?: string;
}

export abstract class AspNetUserRolesRow {
    static readonly idProperty = 'UserId';
    static readonly nameProperty = 'UserId';
    static readonly localTextPrefix = 'Identity.AspNetUserRoles';

    static readonly deletePermission = 'Identity:UserRole';
    static readonly insertPermission = 'Identity:UserRole';
    static readonly readPermission = 'Identity:UserRole';
    static readonly updatePermission = 'Identity:UserRole';

    static readonly Fields = fieldsProxy<AspNetUserRolesRow>();
}