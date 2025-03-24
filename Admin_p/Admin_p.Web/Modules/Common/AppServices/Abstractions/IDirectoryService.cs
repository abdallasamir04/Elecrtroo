
namespace Admin_p;

public interface IDirectoryService
{
    AppServices.DirectoryEntry Validate(string username, string password);
}