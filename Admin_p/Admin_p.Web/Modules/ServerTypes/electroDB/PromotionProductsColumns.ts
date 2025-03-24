import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { PromotionProductsRow } from "./PromotionProductsRow";

export interface PromotionProductsColumns {
    PromotionProductId: Column<PromotionProductsRow>;
    PromotionName: Column<PromotionProductsRow>;
    ProductName: Column<PromotionProductsRow>;
    CreatedAt: Column<PromotionProductsRow>;
    UpdatedAt: Column<PromotionProductsRow>;
}

export class PromotionProductsColumns extends ColumnsBase<PromotionProductsRow> {
    static readonly columnsKey = 'electroDB.PromotionProducts';
    static readonly Fields = fieldsProxy<PromotionProductsColumns>();
}