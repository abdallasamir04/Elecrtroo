using Serenity.Services;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = Admin_p.Identity.AspNetUserRow;

namespace Admin_p.Identity;

public interface IAspNetUserDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> { }

public class AspNetUserDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IAspNetUserDeleteHandler
{
    public AspNetUserDeleteHandler(IRequestContext context)
            : base(context)
    {
    }
}