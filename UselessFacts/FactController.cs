using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using UselessFacts.Models;

namespace UselessFacts;

public class FactController
{
    RestClient client;

    public FactController()
    {
        client = new RestClient("https://uselessfacts.jsph.pl");
    }

    public Fact GetRandomFact()
    {
        RestRequest request = new RestRequest("/api/v2/facts/random");
        return GetFact(request);
    }

    public Fact GetDailyFact()
    {
        RestRequest request = new RestRequest("/api/v2/facts/today");
        return GetFact(request);
    }

    private Fact GetFact(RestRequest request)
    {
        var response = client.ExecuteAsync(request);
        Fact? ans = null;
        if(response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string? rawResponse = response.Result.Content;
            if(rawResponse == null) rawResponse = "";
            ans = JsonConvert.DeserializeObject<Fact>(rawResponse);
        }
        return ans ?? new Fact();
    }
}