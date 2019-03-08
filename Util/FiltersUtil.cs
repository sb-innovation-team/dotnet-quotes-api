using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace socialbrothers_quotes_api.Util {
    public class FiltersUtil {
        private static IQueryCollection _query;
        private static IEnumerable<Quote> _quotes;

        public static IEnumerable<Quote> GetFilteredQuotes(IEnumerable<Quote> quotes, IQueryCollection query) {
            _query = query;
            _quotes = quotes;

            ApplySearchParameters();
            ApplyDateFilter();
            ApplyAmountFilter();
            ApplyCustomFilters();
            return _quotes;
        }

        private static void ApplySearchParameters() {
            if (!ShouldApply(PredefinedFilters.Sp)) return;
            var searchString = GetValue(PredefinedFilters.Sp);

            Console.WriteLine("Searching with string: " + searchString);
            _quotes = _quotes.Where(quote =>
                quote.Author.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) ||
                quote.Content.Contains(searchString, StringComparison.InvariantCultureIgnoreCase));
        }

        private static void ApplyDateFilter() {
            if (!ShouldApply(PredefinedFilters.From) && !ShouldApply(PredefinedFilters.To)) return;

            DateTime from = DateTime.MinValue, to = DateTime.MaxValue;

            if (ShouldApply(PredefinedFilters.From))
                DateTime.TryParse(GetValue(PredefinedFilters.From), CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out from);

            if (ShouldApply(PredefinedFilters.To))
                DateTime.TryParse(GetValue(PredefinedFilters.To), CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out to);

            Console.WriteLine($"Filtering dates from {from} to {to}");
            _quotes = _quotes.Where(quote => quote.CreatedAt >= from && quote.CreatedAt <= to);
        }

        private static void ApplyAmountFilter() {
            if (!ShouldApply(PredefinedFilters.AmountFrom) && !ShouldApply(PredefinedFilters.AmountTo)) return;

            int min = int.MinValue, max = int.MaxValue;
            if (ShouldApply(PredefinedFilters.AmountFrom))
                int.TryParse(GetValue(PredefinedFilters.AmountFrom), out min);

            if (ShouldApply(PredefinedFilters.AmountTo))
                int.TryParse(GetValue(PredefinedFilters.AmountTo), out max);

            Console.WriteLine($"Filtering amount from {min} to {max}");
            _quotes = _quotes.Where(quote => quote.Rating >= min && quote.Rating <= max);
        }

        private static void ApplyCustomFilters() {
            var customFilters = _query.Where(x => !Enum.IsDefined(typeof(PredefinedFilters), x.Key));

            foreach (var (property, value) in customFilters) {
                var t = typeof(Quote);
                var p = t.GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (p == null) continue;
                Console.WriteLine($"Applying custom filter for {property}: {value}");
                _quotes = _quotes.Where(quote => {
                    var propertyValue = p.GetValue(quote).ToString();
                    return propertyValue.Equals(value, StringComparison.InvariantCultureIgnoreCase);
                });
            }
        }

        private static bool ShouldApply(PredefinedFilters filter)
            => _query.ContainsKey(filter.ToString().ToLower());

        private static StringValues GetValue(PredefinedFilters filter)
            => _query[filter.ToString().ToLower()];
    }

    public enum PredefinedFilters {
        AmountFrom,
        AmountTo,
        From,
        To,
        Sp
    }
}