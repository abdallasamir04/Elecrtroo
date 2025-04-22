import { SaveRequest, SaveResponse, ServiceOptions, DeleteRequest, DeleteResponse, RetrieveRequest, RetrieveResponse, ListRequest, ListResponse, serviceRequest } from '@serenity-is/corelib';
import { AspNetUserRolesRow } from './AspNetUserRolesRow';

export namespace AspNetUserRolesService {
    export const baseUrl = 'Identity/AspNetUserRoles';

    export declare function Create(request: SaveRequest<AspNetUserRolesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<AspNetUserRolesRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<AspNetUserRolesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<AspNetUserRolesRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<AspNetUserRolesRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<AspNetUserRolesRow>>;

    export const Methods = {
        Create: "Identity/AspNetUserRoles/Create",
        Update: "Identity/AspNetUserRoles/Update",
        Delete: "Identity/AspNetUserRoles/Delete",
        Retrieve: "Identity/AspNetUserRoles/Retrieve",
        List: "Identity/AspNetUserRoles/List"
    } as const;

    [
        'Create', 
        'Update', 
        'Delete', 
        'Retrieve', 
        'List'
    ].forEach(x => {
        (<any>AspNetUserRolesService)[x] = function (r, s, o) { 
            return serviceRequest(baseUrl + '/' + x, r, s, o); 
        };
    });
}