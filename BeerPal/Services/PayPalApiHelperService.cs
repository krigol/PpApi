using BeerPal.Services.ServiceInterfaces;
using PayPal.Api;

namespace BeerPal.Services
{
    public class PayPalApiHelperService : IPayPalApiHelperService
    {
        public APIContext GetApiContext()
        {
            // Authenticate with PayPal
            var config = ConfigManager.Instance.GetProperties();
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);
            return apiContext;
        }
    }
}
