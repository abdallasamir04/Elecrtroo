using Serenity.Services;
using MyRequest = Serenity.Services.SaveRequest<Admin_p.Identity.AspNetUserRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = Admin_p.Identity.AspNetUserRow;

namespace Admin_p.Identity;

public interface IAspNetUserSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> { }

public class AspNetUserSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IAspNetUserSaveHandler
{
    public AspNetUserSaveHandler(IRequestContext context)
            : base(context)
    {
    }
}