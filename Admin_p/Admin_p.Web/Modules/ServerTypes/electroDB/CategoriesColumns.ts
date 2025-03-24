import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { CategoriesRow } from "./CategoriesRow";

export interface CategoriesColumns {
    CategoryId: Column<CategoriesRow>;
    Name: Column<CategoriesRow>;
    Description: Column<CategoriesRow>;
    CreatedAt: Column<CategoriesRow>;
    UpdatedAt: Column<CategoriesRow>;
    ImageUrl: Column<CategoriesRow>;
}

export class CategoriesColumns extends ColumnsBase<CategoriesRow> {
    static readonly columnsKey = 'electroDB.Categories';
    static readonly Fields = fieldsProxy<CategoriesColumns>();
}