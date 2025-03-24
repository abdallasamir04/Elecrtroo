import { SaveRequest, SaveResponse, ServiceOptions, DeleteRequest, DeleteResponse, RetrieveRequest, RetrieveResponse, ListRequest, ListResponse, serviceRequest } from "@serenity-is/corelib";
import { UsersRow } from "./UsersRow";

export namespace UsersService {
    export const baseUrl = 'electroDB/Users';

    export declare function Create(request: SaveRequest<UsersRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<UsersRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<UsersRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<UsersRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<UsersRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<UsersRow>>;

    export const Methods = {
        Create: "electroDB/Users/Create",
        Update: "electroDB/Users/Update",
        Delete: "electroDB/Users/Delete",
        Retrieve: "electroDB/Users/Retrieve",
        List: "electroDB/Users/List"
    } as const;

    [
        'Create', 
        'Update', 
        'Delete', 
        'Retrieve', 
        'List'
    ].forEach(x => {
        (<any>UsersService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}