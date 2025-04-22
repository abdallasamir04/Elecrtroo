using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<Admin_p.Identity.AspNetUsersRow>;
using MyRow = Admin_p.Identity.AspNetUsersRow;

namespace Admin_p.Identity;

public interface IAspNetUsersRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> { }

public class AspNetUsersRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IAspNetUsersRetrieveHandler
{
    public AspNetUsersRetrieveHandler(IRequestContext context)
            : base(context)
    {
    }
}