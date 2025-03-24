using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB;

[ConnectionKey("Electro"), Module("electroDB"), TableName("PromotionProducts")]
[DisplayName("Promotion Products"), InstanceName("Promotion Products")]
[ReadPermission("PromotionProducts")]
[ModifyPermission("PromotionProducts")]
public sealed class PromotionProductsRow : Row<PromotionProductsRow.RowFields>, IIdRow
{
    const string jPromotion = nameof(jPromotion);
    const string jProduct = nameof(jProduct);

    [DisplayName("Promotion Product Id"), Column("PromotionProductID"), Identity, IdProperty]
    public int? PromotionProductId { get => fields.PromotionProductId[this]; set => fields.PromotionProductId[this] = value; }

    [DisplayName("Promotion"), Column("PromotionID"), NotNull, ForeignKey(typeof(PromotionsRow)), LeftJoin(jPromotion)]
    [TextualField(nameof(PromotionName)), ServiceLookupEditor(typeof(PromotionsRow))]
    public int? PromotionId { get => fields.PromotionId[this]; set => fields.PromotionId[this] = value; }

    [DisplayName("Product"), Column("ProductID"), NotNull, ForeignKey(typeof(ProductsRow)), LeftJoin(jProduct)]
    [TextualField(nameof(ProductName)), ServiceLookupEditor(typeof(ProductsRow))]
    public int? ProductId { get => fields.ProductId[this]; set => fields.ProductId[this] = value; }

    [DisplayName("Created At")]
    public DateTime? CreatedAt { get => fields.CreatedAt[this]; set => fields.CreatedAt[this] = value; }

    [DisplayName("Updated At")]
    public DateTime? UpdatedAt { get => fields.UpdatedAt[this]; set => fields.UpdatedAt[this] = value; }

    [DisplayName("Promotion Name"), Origin(jPromotion, nameof(PromotionsRow.Name))]
    public string PromotionName { get => fields.PromotionName[this]; set => fields.PromotionName[this] = value; }

    [DisplayName("Product Name"), Origin(jProduct, nameof(ProductsRow.Name))]
    public string ProductName { get => fields.ProductName[this]; set => fields.ProductName[this] = value; }

    public class RowFields : RowFieldsBase
    {
        public Int32Field PromotionProductId;
        public Int32Field PromotionId;
        public Int32Field ProductId;
        public DateTimeField CreatedAt;
        public DateTimeField UpdatedAt;

        public StringField PromotionName;
        public StringField ProductName;
    }
}