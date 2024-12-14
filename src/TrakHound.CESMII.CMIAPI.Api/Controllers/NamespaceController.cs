using System.Threading.Tasks;
using TrakHound;
using TrakHound.Api;

[TrakHoundApiController("namespaces")]
public class NamespaceController : TrakHoundApiController
{
    [TrakHoundApiQuery]
    public async Task<TrakHoundApiResponse> Query()
    {
        var namespaces = await Client.System.Entities.Objects.ListNamespaces();
        if (!namespaces.IsNullOrEmpty())
        {
            return Ok(namespaces);
        }
        else
        {
            return NotFound("No Namespaces Found");
        }
    }
}
