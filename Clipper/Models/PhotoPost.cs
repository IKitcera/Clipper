using System;
using System.Collections.Generic;
using System.Text;

namespace Clipper.Models
{
    public class PhotoPost
    {
        public string Id { get; set; }

        //public List<byte[]> Images { get; set; }
        public string UserId { get; set; }
        public List<string> Images { get; set; }
        public string TextBelow { get; set; }
        public List<Reaction> Reactions { get; set; }
        public List<Comment> Comments { get; set; }
        public DateTime CreatingTime { get; set; }
    }
    public enum Reaction
    {
        Positive,
        Negative
    }
}
