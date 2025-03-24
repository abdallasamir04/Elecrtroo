using Serenity.Services;
using MyRequest = Serenity.Services.SaveRequest<Admin_p.electroDB.UsersRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = Admin_p.electroDB.UsersRow;

namespace Admin_p.electroDB;

public interface IUsersSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> { }

public class UsersSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IUsersSaveHandler
{
    public UsersSaveHandler(IRequestContext context)
            : base(context)
    {
    }
}