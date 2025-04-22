using Serenity.Services;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = Admin_p.Identity.AspNetUserRolesRow;

namespace Admin_p.Identity;

public interface IAspNetUserRolesDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> { }

public class AspNetUserRolesDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IAspNetUserRolesDeleteHandler
{
    public AspNetUserRolesDeleteHandler(IRequestContext context)
            : base(context)
    {
    }
}