using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Tomaltach.REST
{
    public interface IRestService
    {
        string BaseURI { get; set; }
        string Controller { get; set; }

        Task<string> GET([NotNull] string uri);
        Task<string> POST([NotNull] string uri, [NotNull] string json);
        Task<string> POST<T>([NotNull] string uri, [NotNull] T model);
        Task<string> PUT([NotNull] string uri, [NotNull] string json);
        Task<string> PUT<T>([NotNull] string uri, [NotNull] T model);
    }
}
