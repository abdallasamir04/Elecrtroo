import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { PromotionsRow } from "./PromotionsRow";

export interface PromotionsColumns {
    PromotionId: Column<PromotionsRow>;
    Name: Column<PromotionsRow>;
    Description: Column<PromotionsRow>;
    DiscountPercentage: Column<PromotionsRow>;
    StartDate: Column<PromotionsRow>;
    EndDate: Column<PromotionsRow>;
    IsActive: Column<PromotionsRow>;
    CreatedAt: Column<PromotionsRow>;
    UpdatedAt: Column<PromotionsRow>;
}

export class PromotionsColumns extends ColumnsBase<PromotionsRow> {
    static readonly columnsKey = 'electroDB.Promotions';
    static readonly Fields = fieldsProxy<PromotionsColumns>();
}