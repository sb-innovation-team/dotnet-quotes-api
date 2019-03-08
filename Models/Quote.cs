using System;
using socialbrothers_quotes_api.Models;

namespace socialbrothers_quotes_api {
    public class Quote : ITrackable {
        public string Author { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}