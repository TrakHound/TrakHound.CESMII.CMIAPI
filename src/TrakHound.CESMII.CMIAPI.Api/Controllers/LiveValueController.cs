using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrakHound;
using TrakHound.Api;
using TrakHound.CESMII.CMIAPI.Api.Models;
using TrakHound.Requests;

[TrakHoundApiController("live")]
public class LiveValueController : TrakHoundApiController
{
    [TrakHoundApiQuery]
    public async Task<TrakHoundApiResponse> Query(
        [FromQuery] string namespaceUri,
        [FromQuery] string elementId = null,
        [FromBody] string[] elementIds = null,
        [FromQuery] bool includeMetadata = false
        )
    {
        var ids = !string.IsNullOrEmpty(elementId) ? new string[] { elementId } : elementIds;
        var paths = ids?.Select(o => TrakHoundPath.Combine(namespaceUri, $"uuid={o}"));
        if (!paths.IsNullOrEmpty())
        {
            var observations = await Client.Entities.GetLatestObservations(paths);
            if (!observations.IsNullOrEmpty())
            {
                var models = new List<LiveValueModel>();

                foreach (var observation in observations)
                {
                    var model = new LiveValueModel();
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

    [TrakHoundApiSubscribe]
    public async Task<ITrakHoundConsumer<TrakHoundApiResponse>> Subscribe(
    [FromQuery] string namespaceUri,
    [FromQuery] string elementId = null,
    [FromBody] string[] elementIds = null,
    [FromQuery] bool includeMetadata = false
    )
    {
        var ids = !string.IsNullOrEmpty(elementId) ? new string[] { elementId } : elementIds;
        //var paths = ids?.Select(o => TrakHoundPath.Combine(namespaceUri, $"uuid={o}")); // Issue with matching query?
        var paths = ids?.Select(o => $"uuid={o}");
        if (!paths.IsNullOrEmpty())
        {
            var observationsConsumer = await Client.Entities.SubscribeObservations(paths);
            if (observationsConsumer != null)
            {
                var outputConsumer = new TrakHoundConsumer<IEnumerable<TrakHoundObservation>, TrakHoundApiResponse>(observationsConsumer);
                outputConsumer.OnReceivedAsync = async (observations) =>
                {
                    var models = new List<LiveValueModel>();

                    foreach (var observation in observations)
                    {
                        var model = new LiveValueModel();
                        model.Value = observation.Value;
                        model.DataType = observation.DataType.ToString();
                        models.Add(model);
                    }

                    return Ok(models);
                };
                return outputConsumer;
            }
        }

        return null;
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
