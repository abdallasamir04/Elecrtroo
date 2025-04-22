using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<Admin_p.Identity.AspNetUserRow>;
using MyRow = Admin_p.Identity.AspNetUserRow;

namespace Admin_p.Identity;

public interface IAspNetUserRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> { }

public class AspNetUserRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IAspNetUserRetrieveHandler
{
    public AspNetUserRetrieveHandler(IRequestContext context)
            : base(context)
    {
    }
}