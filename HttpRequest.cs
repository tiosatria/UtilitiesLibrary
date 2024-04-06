using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace UtilitiesLibrary
{
    public class HTTPRequest
    {

        public enum HttpContentTypeEnum
        {
            STRING,
            FORM_URL_ENCODED
        }

        public HTTPRequest(EndpointURL endpoint) => this.Endpoint = endpoint;

        public EndpointURL Endpoint { get; init; }
        public Dictionary<string, string>? Parameters { get; set; }
        public Dictionary<string, string>? headers { get; set; }
        public string? contentAccept { get; set; }
        public object? content { get; set; }
        public HttpContentTypeEnum? HttpContentType { get; set; }

        public async Task<T?> RequestAsync<T>()
        {
            using (var client = new HttpClient())
            {
                HttpContent? httpContent = null;
                client.DefaultRequestHeaders.Accept.Clear();
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                if (contentAccept != null)
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentAccept));
                }
                if (content is not null && contentAccept is not null)
                {
                    switch (HttpContentType)
                    {
                        case HttpContentTypeEnum.STRING:
                            httpContent = new StringContent(content.ToString() ?? string.Empty, Encoding.UTF8, contentAccept);
                            break;
                        case HttpContentTypeEnum.FORM_URL_ENCODED:
                            httpContent = new FormUrlEncodedContent((List<KeyValuePair<string, string>>)content);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                }
                if (Parameters != null)
                {
                    var idx = 0;
                    foreach (var param in Parameters)
                    {
                        Endpoint.url += $"{(idx == 0 ? "?" : "&")}{param.Key}={param.Value}";
                        idx++;
                    }
                }
                HttpResponseMessage response;
                switch (Endpoint.Method)
                {
                    case METHODENUM.GET:
                        response = await client.GetAsync(Endpoint.url);
                        break;
                    case METHODENUM.POST:
                        response = await client.PostAsync(Endpoint.url, httpContent);
                        break;
                    case METHODENUM.PUT:
                        response = await client.PutAsync(Endpoint.url, httpContent);
                        break;
                    case METHODENUM.DELETE:
                        response = await client.DeleteAsync(Endpoint.url);
                        break;
                    case METHODENUM.PATCH:
                        response = await client.PatchAsync(Endpoint.url, httpContent);
                        break;
                    default:
                        return default;
                }
                if (!response.IsSuccessStatusCode) return default;
                var contentString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(contentString);
            }
        }

        public class EndpointURL
        {
            public string url { get; set; } = string.Empty;
            public METHODENUM Method { get; init; }
        }

        public enum METHODENUM
        {
            GET,
            POST,
            PUT,
            DELETE,
            PATCH
        }

    }

}
