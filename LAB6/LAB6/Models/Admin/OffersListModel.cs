using LAB6.Entities;

namespace LAB6.Models.Admin;

public class OffersListModel
{
    public List<OfferView?>? Offers { get; set; }
    public Guid? idOfSelectedOffer { get; set; }
}