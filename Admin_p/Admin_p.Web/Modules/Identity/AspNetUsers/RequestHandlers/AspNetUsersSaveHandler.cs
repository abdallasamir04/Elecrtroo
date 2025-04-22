using Serenity.Services;
using MyRequest = Serenity.Services.SaveRequest<Admin_p.Identity.AspNetUsersRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = Admin_p.Identity.AspNetUsersRow;

namespace Admin_p.Identity;

public interface IAspNetUsersSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> { }

public class AspNetUsersSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IAspNetUsersSaveHandler
{
    public AspNetUsersSaveHandler(IRequestContext context)
            : base(context)
    {
    }
}