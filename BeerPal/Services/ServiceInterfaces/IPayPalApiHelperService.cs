using PayPal.Api;

namespace BeerPal.Services.ServiceInterfaces
{
    public interface IPayPalApiHelperService
    {
        APIContext GetApiContext();
    }
}
