import { SaveRequest, SaveResponse, ServiceOptions, DeleteRequest, DeleteResponse, RetrieveRequest, RetrieveResponse, ListRequest, ListResponse, serviceRequest } from '@serenity-is/corelib';
import { AspNetUserRow } from './AspNetUserRow';

export namespace AspNetUserService {
    export const baseUrl = 'Identity/AspNetUser';

    export declare function Create(request: SaveRequest<AspNetUserRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<AspNetUserRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<AspNetUserRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<AspNetUserRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<AspNetUserRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<AspNetUserRow>>;

    export const Methods = {
        Create: "Identity/AspNetUser/Create",
        Update: "Identity/AspNetUser/Update",
        Delete: "Identity/AspNetUser/Delete",
        Retrieve: "Identity/AspNetUser/Retrieve",
        List: "Identity/AspNetUser/List"
    } as const;

    [
        'Create', 
        'Update', 
        'Delete', 
        'Retrieve', 
        'List'
    ].forEach(x => {
        (<any>AspNetUserService)[x] = function (r, s, o) { 
            return serviceRequest(baseUrl + '/' + x, r, s, o); 
        };
    });
}