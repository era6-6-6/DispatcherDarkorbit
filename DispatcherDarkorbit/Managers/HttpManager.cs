using System.Net;
using System.Text;
using Krypton_Core;

namespace Dispatcher
{
    public class HttpManager
    {
        private HttpClient _httpClient;

        public CookieContainer _cookies;

        private RequestPerMinuteManager _requestPerMinuteManager;

        public bool LoggedIn { get; set; }

        public HttpManager()
        {
            _requestPerMinuteManager = new RequestPerMinuteManager(45, 45);

            _cookies = new CookieContainer();
            _httpClient = new HttpClient(new HttpClientHandler() { CookieContainer = _cookies, UseCookies = true, AllowAutoRedirect = true });

            _httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "BigpointClient/1.6.7");
        }

        public void SetCookie(string key, string value, string domain)
        {
            _cookies.Add(new Cookie(key, value) { Domain = new Uri(domain).Host });
        }

        public async Task<string> GetAsyncLimit(string url)
        {
            await _requestPerMinuteManager.WaitForNextRequest();
            return await GetAsyncNoLimit(url);
        }

        public async Task<string> GetAsyncNoLimit(string url)
        {
            var data = await _httpClient.GetAsync(url);

            return await ReadResponseAsync(data);
        }

        public async Task<HttpResponseMessage> PostAsyncLimitRaw(string url, string postData)
        {
            await _requestPerMinuteManager.WaitForNextRequest();
            var data = await _httpClient.PostAsync(url,
                new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded"));

            return data;
        }

        public async Task<string> ReadResponseAsync(HttpResponseMessage response)
        {
            var data = await response.Content.ReadAsStringAsync();
            if (!data.Contains("dosid") && LoggedIn)
            {
                if (data.Contains("session_invalid")) //gate/flash api doesnt contain dosid
                {
                    LoggedIn = false;

                }
                else if (response.RequestMessage.RequestUri.AbsoluteUri.ToLower().Contains("loginerror"))
                {
                    LoggedIn = false;

                }
            }

            return data;
        }
    }
}