using System;

namespace socialbrothers_quotes_api.Models {
    public interface ITrackable {
        long Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}