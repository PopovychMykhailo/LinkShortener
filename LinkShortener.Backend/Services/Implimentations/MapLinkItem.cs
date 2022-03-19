using LinkShortener.Backend.Domain.Entities.Implimentations;
using LinkShortener.Backend.Models;
using LinkShortener.Backend.Services.Interfaces;
using System;

namespace LinkShortener.Backend.Services.Implimentations
{
    public class MapLinkItem<TSource, TDest>
    {
        public LinkItem Mapping(LinkItemModel model)
        {
            return new LinkItem()
            {
                UserId = Guid.Empty,
                Id = (Guid)model.Id,
                ShortLink = model.ShortLink,
                LongLink = model.LongLink,
                CreatedDate = (DateTime) model.CreatedDate
            };
        }

        //public LinkItemModel Mapping(LinkItem entity)
        //{

        //}
    }
}
