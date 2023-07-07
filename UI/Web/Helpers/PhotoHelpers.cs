using Microsoft.Extensions.Options;
using Web.Models;

namespace Web.Helpers
{
    public class PhotoHelpers
    {
        private readonly ServiceApiSettings _serviceApiSettings;

        public PhotoHelpers(IOptions< ServiceApiSettings > serviceApiSettings)
        {
            _serviceApiSettings = serviceApiSettings.Value;
        }
        public string PhotoStockUrl(string Url)
        {
            return $"{_serviceApiSettings.PhotoUrl}/Images/{Url}";

        }

    }
}
