using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System.ComponentModel;

namespace Admin_p.Identity;

[ConnectionKey("Electro"), Module("Identity"), TableName("AspNetRoles")]
[DisplayName("Asp Net User"), InstanceName("Asp Net User")]
[ReadPermission("Identity:Role")]
[ModifyPermission("Identity:Role")]
[ServiceLookupPermission("Identity:Role")]
public sealed class AspNetUserRow : Row<AspNetUserRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), Size(450), PrimaryKey, NotNull, IdProperty, QuickSearch, NameProperty]
    public string Id { get => fields.Id[this]; set => fields.Id[this] = value; }

    [DisplayName("Name"), Size(256)]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }

    [DisplayName("Normalized Name"), Size(256)]
    public string NormalizedName { get => fields.NormalizedName[this]; set => fields.NormalizedName[this] = value; }

    [DisplayName("Concurrency Stamp")]
    public string ConcurrencyStamp { get => fields.ConcurrencyStamp[this]; set => fields.ConcurrencyStamp[this] = value; }

    public class RowFields : RowFieldsBase
    {
        public StringField Id;
        public StringField Name;
        public StringField NormalizedName;
        public StringField ConcurrencyStamp;

    }
}