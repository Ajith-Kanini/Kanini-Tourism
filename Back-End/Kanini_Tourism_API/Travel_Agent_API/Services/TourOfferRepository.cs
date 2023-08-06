using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgencyManagementAPI.Models;
using TravelAgencyManagementAPI.DB;

namespace TravelAgencyManagementAPI.Repositories
{
    public class TourOfferRepository : ITourOfferRepository
    {
        private readonly AgencyContext _context;

        public TourOfferRepository(AgencyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TourOffer>> GetTourOffersAsync()
        {
            return await _context.TourOffers.ToListAsync();
        }

        public async Task<TourOffer> GetTourOfferByIdAsync(int id)
        {
            return await _context.TourOffers.FindAsync(id);
        }

        public async Task<TourOffer> AddTourOfferAsync(TourOffer tourOffer)
        {
            if (tourOffer == null)
            {
                throw new ArgumentNullException(nameof(tourOffer));
            }

            _context.TourOffers.Add(tourOffer);
            await _context.SaveChangesAsync();

            return tourOffer;
        }

        public async Task<TourOffer> UpdateTourOfferAsync(int id, TourOffer tourOffer)
        {
            if (tourOffer == null)
            {
                throw new ArgumentNullException(nameof(tourOffer));
            }

            var existingTourOffer = await _context.TourOffers.FindAsync(id);
            if (existingTourOffer == null)
            {
                return null;
            }

            existingTourOffer.OfferName = tourOffer.OfferName;
            existingTourOffer.Description = tourOffer.Description;
            // Update other properties

            _context.Entry(existingTourOffer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingTourOffer;
        }

        public async Task DeleteTourOfferAsync(TourOffer tourOffer)
        {
            if (tourOffer == null)
            {
                throw new ArgumentNullException(nameof(tourOffer));
            }

            _context.TourOffers.Remove(tourOffer);
            await _context.SaveChangesAsync();
        }
    }
}
