using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tomaltach.REST
{
    public abstract class RestService : IRestService
    {
        private const string APPJSON = "application/json";
        private HttpClient _httpClient = new HttpClient();

        public abstract string BaseURI { get; set; }
        public abstract string Controller { get; set; }

        public async Task<string> GET([NotNull] string uri)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            if (BaseURI == null) throw new ArgumentNullException(nameof(BaseURI));
            if (Controller == null) throw new ArgumentNullException(nameof(Controller));

            try
            {
                _httpClient.BaseAddress = new Uri(BaseURI);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APPJSON));
                var fullUri = $"api/{Controller}/{uri}";
                var response = _httpClient.GetAsync(fullUri);
                var results = await response.Result.Content.ReadAsStringAsync();
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> POST([NotNull] string uri, [NotNull] string json)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            if (json == null) throw new ArgumentNullException(nameof(json));
            if (BaseURI == null) throw new ArgumentNullException(nameof(BaseURI));
            if (Controller == null) throw new ArgumentNullException(nameof(Controller));

            try
            {
                _httpClient.BaseAddress = new Uri(BaseURI);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APPJSON));
                var fullUri = $"api/{Controller}/{uri}";
                var content = new StringContent(json, Encoding.UTF8, APPJSON);
                var response = _httpClient.PostAsync(fullUri, content);
                var results = await response.Result.Content.ReadAsStringAsync();
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> POST<T>([NotNull] string uri, [NotNull] T model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var json = JsonConvert.SerializeObject(model);

            return await POST(uri, json);
        }

        public async Task<string> PUT([NotNull] string uri, [NotNull] string json)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            if (json == null) throw new ArgumentNullException(nameof(json));
            if (BaseURI == null) throw new ArgumentNullException(nameof(BaseURI));
            if (Controller == null) throw new ArgumentNullException(nameof(Controller));

            try
            {
                _httpClient.BaseAddress = new Uri(BaseURI);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APPJSON));
                var fullUri = $"api/{Controller}/{uri}";
                var content = new StringContent(json, Encoding.UTF8, APPJSON);
                var response = _httpClient.PutAsync(fullUri, content);
                var results = await response.Result.Content.ReadAsStringAsync();
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> PUT<T>([NotNull] string uri, [NotNull] T model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var json = JsonConvert.SerializeObject(model);

            return await PUT(uri, json);
        }
    }
}
