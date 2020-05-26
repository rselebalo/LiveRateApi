using Microsoft.EntityFrameworkCore;

public class RatesDbContext : DbContext
{
    public RatesDbContext(DbContextOptions<RatesDbContext> options) : base(options)
    {

    }
    public DbSet<RatesDataModel> Rates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RatesDataModel>(entity =>
        {
            entity.HasKey(e => e.id);
            entity.Property(e => e.currency).IsRequired();
            entity.Property(e => e.rate).IsRequired();
            entity.Property(e => e.bid).IsRequired();
            entity.Property(e => e.ask).IsRequired();
            entity.Property(e => e.high).IsRequired();
            entity.Property(e => e.low).IsRequired();
            entity.Property(e => e.open).IsRequired();
            entity.Property(e => e.close).IsRequired();
            entity.Property(e => e.timestamp).IsRequired();
        });
    }
}