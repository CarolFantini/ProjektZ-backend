using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class RelationalDbContext : DbContext
    {
        public RelationalDbContext(DbContextOptions<RelationalDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name)
                    .IsRequired();
            });

            modelBuilder.Entity<Series>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name)
                    .IsRequired();
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Name)
                    .IsRequired();
                entity.Property(b => b.Pages)
                    .IsRequired();
                entity.Property(b => b.Genres)
                    .IsRequired();
                entity.Property(b => b.Publisher)
                    .IsRequired();
                entity.Property(b => b.Format)
                    .IsRequired();
                entity.Property(b => b.Status)
                    .IsRequired();
                entity.Property(b => b.Price)
                    .HasPrecision(10, 2);

                entity.HasOne(b => b.Series)
                      .WithMany()
                      .HasForeignKey("SeriesId")
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(b => b.Authors)
                      .WithMany()
                      .UsingEntity(ba => ba.ToTable("BookAuthors"));
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.GrossSalary)
                    .HasPrecision(10, 2);
                entity.Property(i => i.INSSDiscount)
                    .HasPrecision(10, 2);
                entity.Property(i => i.IRDiscount)
                    .HasPrecision(10, 2);
                entity.Property(i => i.PLR)
                    .HasPrecision(10, 2);
                entity.Property(i => i.VAorVR)
                    .HasPrecision(10, 2);
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount)
                    .HasPrecision(10, 2);
            });
        }

        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
