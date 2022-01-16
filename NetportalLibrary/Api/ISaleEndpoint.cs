using EPSDesktopUI.Library.Models;
using System.Threading.Tasks;

namespace EPSDesktopUI.Library.Api
{
    public interface ISaleEndpoint
    {
        Task PostSale(SaleModel sale);
    }
}