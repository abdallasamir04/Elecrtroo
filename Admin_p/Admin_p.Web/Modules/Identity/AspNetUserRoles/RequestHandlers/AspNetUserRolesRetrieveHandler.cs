using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<Admin_p.Identity.AspNetUserRolesRow>;
using MyRow = Admin_p.Identity.AspNetUserRolesRow;

namespace Admin_p.Identity;

public interface IAspNetUserRolesRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> { }

public class AspNetUserRolesRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IAspNetUserRolesRetrieveHandler
{
    public AspNetUserRolesRetrieveHandler(IRequestContext context)
            : base(context)
    {
    }
}