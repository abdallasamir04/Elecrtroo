import { SaveRequest, SaveResponse, ServiceOptions, DeleteRequest, DeleteResponse, RetrieveRequest, RetrieveResponse, ListRequest, ListResponse, serviceRequest } from "@serenity-is/corelib";
import { PaymentsRow } from "./PaymentsRow";

export namespace PaymentsService {
    export const baseUrl = 'electroDB/Payments';

    export declare function Create(request: SaveRequest<PaymentsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<PaymentsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<PaymentsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<PaymentsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<PaymentsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<PaymentsRow>>;

    export const Methods = {
        Create: "electroDB/Payments/Create",
        Update: "electroDB/Payments/Update",
        Delete: "electroDB/Payments/Delete",
        Retrieve: "electroDB/Payments/Retrieve",
        List: "electroDB/Payments/List"
    } as const;

    [
        'Create', 
        'Update', 
        'Delete', 
        'Retrieve', 
        'List'
    ].forEach(x => {
        (<any>PaymentsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}