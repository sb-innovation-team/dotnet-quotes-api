using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace socialbrothers_quotes_api.Models {
    public class QuoteContext : DbContext {
        public QuoteContext(DbContextOptions<QuoteContext> options) : base(options) {
        }

        public DbSet<Quote> Quotes { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess) {
            SetTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken)) {
            SetTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SetTimestamps() {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
                if (entry.Entity is ITrackable trackable) {
                    var now = DateTime.UtcNow;
                    switch (entry.State) {
                        case EntityState.Modified:
                            trackable.UpdatedAt = now;
                            break;

                        case EntityState.Added:
                            trackable.CreatedAt = now;
                            trackable.UpdatedAt = now;
                            break;
                    }
                }
        }
    }
}