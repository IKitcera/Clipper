using System;
using System.Collections.Generic;
using System.Text;

namespace Clipper.Models
{
    public class ReactionItem
    {
        public string Id { get; set; }
        public Reaction Reaction { get; set; }
        public string UserLeftedId { get; set; }
        public string PostId { get; set; }
    }

    public enum Reaction
    {
        Positive,
        Negative
    }
}
