using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System.ComponentModel;

namespace Admin_p.Identity;

[ConnectionKey("Electro"), Module("Identity"), TableName("AspNetUserRoles")]
[DisplayName("Asp Net User Roles"), InstanceName("Asp Net User Roles")]
[ReadPermission("Identity:UserRole")]
[ModifyPermission("Identity:UserRole")]
public sealed class AspNetUserRolesRow : Row<AspNetUserRolesRow.RowFields>, IIdRow, INameRow
{
    const string jUser = nameof(jUser);
    const string jRole = nameof(jRole);

    [DisplayName("User"), Size(450), PrimaryKey, NotNull, ForeignKey(typeof(AspNetUsersRow)), LeftJoin(jUser), IdProperty, QuickSearch]
    [NameProperty, TextualField(nameof(UserFirstName)), ServiceLookupEditor(typeof(AspNetUsersRow))]
    public string UserId { get => fields.UserId[this]; set => fields.UserId[this] = value; }

    [DisplayName("Role"), Size(450), PrimaryKey, NotNull, ForeignKey(typeof(AspNetUserRow)), LeftJoin(jRole), TextualField(nameof(RoleName))]
    [ServiceLookupEditor(typeof(AspNetUserRow))]
    public string RoleId { get => fields.RoleId[this]; set => fields.RoleId[this] = value; }

    [DisplayName("User First Name"), Origin(jUser, nameof(AspNetUsersRow.FirstName))]
    public string UserFirstName { get => fields.UserFirstName[this]; set => fields.UserFirstName[this] = value; }

    [DisplayName("Role Name"), Origin(jRole, nameof(AspNetUserRow.Name))]
    public string RoleName { get => fields.RoleName[this]; set => fields.RoleName[this] = value; }

    public class RowFields : RowFieldsBase
    {
        public StringField UserId;
        public StringField RoleId;

        public StringField UserFirstName;
        public StringField RoleName;
    }
}