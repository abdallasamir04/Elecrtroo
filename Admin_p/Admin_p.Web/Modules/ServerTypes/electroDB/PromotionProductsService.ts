import { SaveRequest, SaveResponse, ServiceOptions, DeleteRequest, DeleteResponse, RetrieveRequest, RetrieveResponse, ListRequest, ListResponse, serviceRequest } from "@serenity-is/corelib";
import { PromotionProductsRow } from "./PromotionProductsRow";

export namespace PromotionProductsService {
    export const baseUrl = 'electroDB/PromotionProducts';

    export declare function Create(request: SaveRequest<PromotionProductsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<PromotionProductsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<PromotionProductsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<PromotionProductsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<PromotionProductsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<PromotionProductsRow>>;

    export const Methods = {
        Create: "electroDB/PromotionProducts/Create",
        Update: "electroDB/PromotionProducts/Update",
        Delete: "electroDB/PromotionProducts/Delete",
        Retrieve: "electroDB/PromotionProducts/Retrieve",
        List: "electroDB/PromotionProducts/List"
    } as const;

    [
        'Create', 
        'Update', 
        'Delete', 
        'Retrieve', 
        'List'
    ].forEach(x => {
        (<any>PromotionProductsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}