using System.Threading.Tasks;
using TrakHound;
using TrakHound.Api;

[TrakHoundApiController("types")]
public class TypesController : TrakHoundApiController
{
    private const string _typesBasePath = "CESMII.CMIAPI:/.Types";


    [TrakHoundApiQuery]
    public async Task<TrakHoundApiResponse> Query()
    {
        var types = await Client.Entities.GetStrings($"{_typesBasePath}/*");
        if (!types.IsNullOrEmpty())
        {
            return Ok(types);
        }
        else
        {
            return NotFound("No Types Found");
        }
    }

    [TrakHoundApiQuery("{elementId}")]
    public async Task<TrakHoundApiResponse> Query([FromRoute] string elementId)
    {
        var type = await Client.Entities.GetString($"{_typesBasePath}/{elementId}");
        if (type != null)
        {
            return Ok(type);
        }
        else
        {
            return NotFound("Type Not Found");
        }
    }

    [TrakHoundApiQuery("{elementId}")]
    public async Task<TrakHoundApiResponse> Write([FromRoute] string elementId, [FromBody] string typeJson)
    {
        if (await Client.Entities.PublishString($"{_typesBasePath}/{elementId}", typeJson))
        {
            return Ok(typeJson);
        }
        else
        {
            return InternalError("Error Writing Type");
        }
    }
}
