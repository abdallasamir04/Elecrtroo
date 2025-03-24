using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace Admin_p.electroDB;

[ConnectionKey("Electro"), Module("electroDB"), TableName("Categories")]
[DisplayName("Categories"), InstanceName("Categories")]
[ReadPermission("Categories")]
[ModifyPermission("Categories")]
[ServiceLookupPermission("Categories")]
public sealed class CategoriesRow : Row<CategoriesRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Category Id"), Column("CategoryID"), Identity, IdProperty]
    public int? CategoryId { get => fields.CategoryId[this]; set => fields.CategoryId[this] = value; }

    [DisplayName("Name"), Size(100), NotNull, QuickSearch, NameProperty]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }

    [DisplayName("Description")]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }

    [DisplayName("Created At")]
    public DateTime? CreatedAt { get => fields.CreatedAt[this]; set => fields.CreatedAt[this] = value; }

    [DisplayName("Updated At")]
    public DateTime? UpdatedAt { get => fields.UpdatedAt[this]; set => fields.UpdatedAt[this] = value; }

    [DisplayName("Image Url"), Size(255)]
    public string ImageUrl { get => fields.ImageUrl[this]; set => fields.ImageUrl[this] = value; }

    public class RowFields : RowFieldsBase
    {
        public Int32Field CategoryId;
        public StringField Name;
        public StringField Description;
        public DateTimeField CreatedAt;
        public DateTimeField UpdatedAt;
        public StringField ImageUrl;

    }
}