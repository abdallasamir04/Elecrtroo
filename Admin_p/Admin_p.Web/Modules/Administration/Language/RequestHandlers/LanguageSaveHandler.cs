using MyRequest = Serenity.Services.SaveRequest<Admin_p.Administration.LanguageRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = Admin_p.Administration.LanguageRow;


namespace Admin_p.Administration;
public interface ILanguageSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> { }
public class LanguageSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, ILanguageSaveHandler
{
    public LanguageSaveHandler(IRequestContext context)
         : base(context)
    {
    }
}