﻿using SuperMarket.Models.DataAccessLayer;
using SuperMarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Models.BusinessLogicLayer
{
    public class OfferBLL
    {
        private OfferDAL _offerDAL = new OfferDAL();
        public ObservableCollection<Offer> GetAllOffers()
        {
            return _offerDAL.GetAllOffers();
        }

        public void DeleteOffer(Offer offer)
        {
            _offerDAL.DeleteOffer(offer);
        }

        public void UpdateOffer(Offer offer)
        {
            _offerDAL.ModifyOffer(offer);
        }

        public void AddOffer(Offer offer)
        {
            _offerDAL.AddOffer(offer);
        }
    }
}
