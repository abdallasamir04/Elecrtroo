using Serenity.Services;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = Admin_p.Identity.AspNetUsersRow;

namespace Admin_p.Identity;

public interface IAspNetUsersDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> { }

public class AspNetUsersDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IAspNetUsersDeleteHandler
{
    public AspNetUsersDeleteHandler(IRequestContext context)
            : base(context)
    {
    }
}