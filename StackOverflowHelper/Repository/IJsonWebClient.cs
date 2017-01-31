using System.Net;
using System.Threading.Tasks;

namespace StackOverflowHelper.Repository
{
    public interface IJsonWebClient
    {
        Task<string> HttpGetUncompressedAsync(string url);
    }
}