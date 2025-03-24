import { SaveRequest, SaveResponse, ServiceOptions, DeleteRequest, DeleteResponse, RetrieveRequest, RetrieveResponse, ListRequest, ListResponse, serviceRequest } from "@serenity-is/corelib";
import { ShoppingCartsRow } from "./ShoppingCartsRow";

export namespace ShoppingCartsService {
    export const baseUrl = 'electroDB/ShoppingCarts';

    export declare function Create(request: SaveRequest<ShoppingCartsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<ShoppingCartsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<ShoppingCartsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<ShoppingCartsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<ShoppingCartsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<ShoppingCartsRow>>;

    export const Methods = {
        Create: "electroDB/ShoppingCarts/Create",
        Update: "electroDB/ShoppingCarts/Update",
        Delete: "electroDB/ShoppingCarts/Delete",
        Retrieve: "electroDB/ShoppingCarts/Retrieve",
        List: "electroDB/ShoppingCarts/List"
    } as const;

    [
        'Create', 
        'Update', 
        'Delete', 
        'Retrieve', 
        'List'
    ].forEach(x => {
        (<any>ShoppingCartsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}