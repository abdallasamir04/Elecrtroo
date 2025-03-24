import { SaveRequest, SaveResponse, ServiceOptions, DeleteRequest, DeleteResponse, RetrieveRequest, RetrieveResponse, ListRequest, ListResponse, serviceRequest } from "@serenity-is/corelib";
import { PromotionsRow } from "./PromotionsRow";

export namespace PromotionsService {
    export const baseUrl = 'electroDB/Promotions';

    export declare function Create(request: SaveRequest<PromotionsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<PromotionsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<PromotionsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<PromotionsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<PromotionsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<PromotionsRow>>;

    export const Methods = {
        Create: "electroDB/Promotions/Create",
        Update: "electroDB/Promotions/Update",
        Delete: "electroDB/Promotions/Delete",
        Retrieve: "electroDB/Promotions/Retrieve",
        List: "electroDB/Promotions/List"
    } as const;

    [
        'Create', 
        'Update', 
        'Delete', 
        'Retrieve', 
        'List'
    ].forEach(x => {
        (<any>PromotionsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}