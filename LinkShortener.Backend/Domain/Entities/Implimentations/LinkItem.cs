using System;
using LinkShortener.Backend.Domain.Entities.Interfaces;

namespace LinkShortener.Backend.Domain.Entities.Implimentations
{
    public class LinkItem : EntityBase
    {
        public Guid UserId { get; set; }
        public string ShortLink { get; set; }
        public string LongLink { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
