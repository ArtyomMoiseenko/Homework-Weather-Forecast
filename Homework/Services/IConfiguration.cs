namespace Homework.Services
{
    public interface IConfiguration
    {
        string BaseUrl { get; set; }
        string ApiKey { get; set; }
    }
}
