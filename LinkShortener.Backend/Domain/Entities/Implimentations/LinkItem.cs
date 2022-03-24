using System;
using System.ComponentModel.DataAnnotations;
using LinkShortener.Resource.Domain.Entities.Interfaces;

namespace LinkShortener.Resource.Domain.Entities.Implimentations
{
    public class LinkItem : EntityBase
    {
        [Required]
        public Guid UserId { get; set; }
        public string ShortLink { get; set; }
        public string LongLink { get; set; }
        public DateTime CreatedDate { get; set; }



        public LinkItem(Guid userId, string longLink)
        {
            UserId = userId;
            ShortLink = Guid.NewGuid().ToString();
            LongLink = longLink;
            CreatedDate = DateTime.Now;
        }
    }
}
