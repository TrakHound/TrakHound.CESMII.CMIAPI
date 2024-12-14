using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrakHound;
using TrakHound.Api;
using TrakHound.CESMII.CMIAPI.Api.Models;

[TrakHoundApiController("historical")]
public class HistoricalValueController : TrakHoundApiController
{
    [TrakHoundApiQuery]
    public async Task<TrakHoundApiResponse> Query(
        [FromQuery] string namespaceUri,
        [FromQuery] string from,
        [FromQuery] string to,
        [FromQuery] string elementId = null,
        [FromBody] string[] elementIds = null,
        [FromQuery] bool includeMetadata = false
        )
    {
        var ids = !string.IsNullOrEmpty(elementId) ? new string[] { elementId } : elementIds;
        var paths = ids?.Select(o => TrakHoundPath.Combine(namespaceUri, $"uuid={o}"));
        if (!paths.IsNullOrEmpty())
        {
            var observations = await Client.Entities.GetObservations(paths, from, to);
            if (!observations.IsNullOrEmpty())
            {
                var models = new List<HistoricalValueModel>();

                foreach (var observation in observations)
                {
                    var model = new HistoricalValueModel();
                    model.Value = observation.Value;
                    model.DataType = observation.DataType.ToString();
                    models.Add(model);
                }

                return Ok(models);
            }
            else
            {
                return NotFound();
            }
        }
        else
        {
            return BadRequest();
        }
    }

    [TrakHoundApiPublish]
    public async Task<TrakHoundApiResponse> Update(
    [FromQuery] string namespaceUri,
    [FromQuery] string elementId,
    [FromQuery] LiveValueModel value = null,
    [FromBody] LiveValueModel[] values = null
    )
    {
        return Ok("TrakHound.CESMII.CMIAPI.Api : Query : " + System.DateTime.Now.ToString("o"));
    }
}
