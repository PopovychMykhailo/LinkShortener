using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.Backend.Models
{
    public class LinkItemModel
    {
        public Guid Id { get; set; }
        public string LongLink { get; set; }

        public string? ShortLink { get; }
        public DateTime? CreatedDate { get; }
    }
}
