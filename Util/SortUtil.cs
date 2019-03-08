using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace socialbrothers_quotes_api.Util {
    public class SortUtil {
        public enum SortingOrder {
            Desc,
            Asc
        }

        public static IEnumerable<Quote> GetSortedQuotes(IEnumerable<Quote> quotes, IQueryCollection query) {
            if (!query.ContainsKey("sort")) return quotes;

            var property = query["sort"].ToString();
            var order = query["order"].ToString();

            return Sort(quotes, property, order).ToList();
        }

        private static IEnumerable<Quote> Sort(IEnumerable<Quote> quotes, string property, string order) {
            var sortingOrder = SortingOrder.Asc;
            if (!string.IsNullOrWhiteSpace(order))
                Enum.TryParse(order, true, out sortingOrder);

            var t = typeof(Quote);
            var p = t.GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (p == null) return quotes;
            Console.WriteLine($"Sorting collection on {p.Name} {sortingOrder}");

            switch (sortingOrder) {
                case SortingOrder.Desc:
                    return quotes.OrderByDescending(quote => p.GetValue(quote));
                case SortingOrder.Asc:
                default:
                    return quotes.OrderBy(quote => p.GetValue(quote));
            }
        }
    }
}