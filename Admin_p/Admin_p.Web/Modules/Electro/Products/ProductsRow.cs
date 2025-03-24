using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace Admin_p.Electro;

[ConnectionKey("Electro"), Module("Electro"), TableName("Products")]
[DisplayName("Products"), InstanceName("Products")]
[ReadPermission("Products")]
[ModifyPermission("Products")]
[ServiceLookupPermission("Products")]
public sealed class ProductsRow : Row<ProductsRow.RowFields>, IIdRow, INameRow
{
    const string jCategory = nameof(jCategory);

    [DisplayName("Product Id"), Column("ProductID"), Identity, IdProperty]
    public int? ProductId { get => fields.ProductId[this]; set => fields.ProductId[this] = value; }

    [DisplayName("Name"), Size(100), NotNull, QuickSearch, NameProperty]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }

    [DisplayName("Description")]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }

    [DisplayName("Price"), Size(18), Scale(2), NotNull]
    public decimal? Price { get => fields.Price[this]; set => fields.Price[this] = value; }

    [DisplayName("Stock Quantity"), NotNull]
    public int? StockQuantity { get => fields.StockQuantity[this]; set => fields.StockQuantity[this] = value; }

    [DisplayName("Category"), Column("CategoryID"), ForeignKey(typeof(electroDB.CategoriesRow)), LeftJoin(jCategory)]
    [TextualField(nameof(CategoryName)), ServiceLookupEditor(typeof(electroDB.CategoriesRow), Service = "electroDB/Categories/List")]
    public int? CategoryId { get => fields.CategoryId[this]; set => fields.CategoryId[this] = value; }

    [DisplayName("Created At")]
    public DateTime? CreatedAt { get => fields.CreatedAt[this]; set => fields.CreatedAt[this] = value; }

    [DisplayName("Updated At")]
    public DateTime? UpdatedAt { get => fields.UpdatedAt[this]; set => fields.UpdatedAt[this] = value; }

    [DisplayName("Image Path"), Size(255)]
    public string ImagePath { get => fields.ImagePath[this]; set => fields.ImagePath[this] = value; }

    [DisplayName("Category Name"), Origin(jCategory, nameof(electroDB.CategoriesRow.Name))]
    public string CategoryName { get => fields.CategoryName[this]; set => fields.CategoryName[this] = value; }

    public class RowFields : RowFieldsBase
    {
        public Int32Field ProductId;
        public StringField Name;
        public StringField Description;
        public DecimalField Price;
        public Int32Field StockQuantity;
        public Int32Field CategoryId;
        public DateTimeField CreatedAt;
        public DateTimeField UpdatedAt;
        public StringField ImagePath;

        public StringField CategoryName;
    }
}