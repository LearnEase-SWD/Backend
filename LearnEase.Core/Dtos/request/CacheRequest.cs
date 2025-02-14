namespace LearnEase_Api.Dtos.request
{
    public record CacheRequest(string key, string value, int time)
    {
    }
}
