using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgencyManagementAPI.Models;

namespace TravelAgencyManagementAPI.Repositories
{
    public interface ITourOfferRepository
    {
        Task<IEnumerable<TourOffer>> GetTourOffersAsync();
        Task<TourOffer> GetTourOfferByIdAsync(int id);
        Task<TourOffer> AddTourOfferAsync(TourOffer tourOffer);
        Task<TourOffer> UpdateTourOfferAsync(int id, TourOffer tourOffer);
        Task DeleteTourOfferAsync(TourOffer tourOffer);
    }
}
