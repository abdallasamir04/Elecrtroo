using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB;

[ConnectionKey("Electro"), Module("electroDB"), TableName("Promotions")]
[DisplayName("Promotions"), InstanceName("Promotions")]
[ReadPermission("Promotions")]
[ModifyPermission("Promotions")]
[ServiceLookupPermission("Promotions")]
public sealed class PromotionsRow : Row<PromotionsRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Promotion Id"), Column("PromotionID"), Identity, IdProperty]
    public int? PromotionId { get => fields.PromotionId[this]; set => fields.PromotionId[this] = value; }

    [DisplayName("Name"), Size(100), NotNull, QuickSearch, NameProperty]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }

    [DisplayName("Description")]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }

    [DisplayName("Discount Percentage"), Size(5), Scale(2)]
    public decimal? DiscountPercentage { get => fields.DiscountPercentage[this]; set => fields.DiscountPercentage[this] = value; }

    [DisplayName("Start Date"), NotNull]
    public DateTime? StartDate { get => fields.StartDate[this]; set => fields.StartDate[this] = value; }

    [DisplayName("End Date"), NotNull]
    public DateTime? EndDate { get => fields.EndDate[this]; set => fields.EndDate[this] = value; }

    [DisplayName("Is Active")]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }

    [DisplayName("Created At")]
    public DateTime? CreatedAt { get => fields.CreatedAt[this]; set => fields.CreatedAt[this] = value; }

    [DisplayName("Updated At")]
    public DateTime? UpdatedAt { get => fields.UpdatedAt[this]; set => fields.UpdatedAt[this] = value; }

    public class RowFields : RowFieldsBase
    {
        public Int32Field PromotionId;
        public StringField Name;
        public StringField Description;
        public DecimalField DiscountPercentage;
        public DateTimeField StartDate;
        public DateTimeField EndDate;
        public BooleanField IsActive;
        public DateTimeField CreatedAt;
        public DateTimeField UpdatedAt;

    }
}