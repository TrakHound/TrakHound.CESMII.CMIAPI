using System.Threading.Tasks;
using System.Xml.Linq;
using TrakHound;
using TrakHound.Api;
using TrakHound.CESMII.CMIAPI.Api.Models;

[TrakHoundApiController("objects")]
public class ObjectsController : TrakHoundApiController
{
    [TrakHoundApiQuery]
    public async Task<TrakHoundApiResponse> Query(
        [FromQuery] string type = null,
        [FromQuery] string elementId = null
        )
    {
        return Ok("TrakHound.CESMII.CMIAPI.Api : Query : " + System.DateTime.Now.ToString("o"));
    }

    [TrakHoundApiQuery("")]
    public async Task<TrakHoundApiResponse> Query()
    {
        return Ok("TrakHound.CESMII.CMIAPI.Api : Query : " + System.DateTime.Now.ToString("o"));
    }
}
