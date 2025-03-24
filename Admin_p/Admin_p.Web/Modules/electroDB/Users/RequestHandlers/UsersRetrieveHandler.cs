using Serenity.Services;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<Admin_p.electroDB.UsersRow>;
using MyRow = Admin_p.electroDB.UsersRow;

namespace Admin_p.electroDB;

public interface IUsersRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> { }

public class UsersRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IUsersRetrieveHandler
{
    public UsersRetrieveHandler(IRequestContext context)
            : base(context)
    {
    }
}