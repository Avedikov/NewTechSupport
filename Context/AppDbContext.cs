using TechSupport.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TechSupport.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options) // Передаем options в базовый класс
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketHistory> TicketHistories { get; set; }

        public DbSet<KnowledgeArticle> KnowledgeArticles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-2G4ET2H\\MSSQLSERVER01;Database=Testes;Trusted_Connection=True;Integrated Security=SSPI;TrustServerCertificate=True;");
            optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигурация для Ticket
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(t => t.CreateDate).HasDefaultValueSql("GETDATE()");
                entity.Property(t => t.LastUpdated).HasDefaultValueSql("GETDATE()");
                
                entity.HasOne(t => t.AssignedUser)
                    .WithMany(u => u.AssignedTickets)
                    .HasForeignKey(t => t.AssignedUserId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(t => t.History)
                    .WithOne(h => h.Ticket)
                    .HasForeignKey(h => h.TicketId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Ticket>()
                    .HasOne(t => t.Author)
                    .WithMany()
                    .HasForeignKey(t => t.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Ticket>()
                    .HasOne(t => t.AssignedUser)
                    .WithMany()
                    .HasForeignKey(t => t.AssignedUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasOne(t => t.Author)
                    .WithMany()
                    .HasForeignKey(t => t.AuthorId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(t => t.AssignedUser)
                    .WithMany()
                    .HasForeignKey(t => t.AssignedUserId)
                    .OnDelete(DeleteBehavior.NoAction);

                
            });

            // Конфигурация для User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();

                entity.HasMany(u => u.TicketHistories)
                    .WithOne(h => h.ChangedByUser)
                    .HasForeignKey(h => h.ChangedByUserId)
                    .OnDelete(DeleteBehavior.SetNull);
                

            });
            modelBuilder.Entity<KnowledgeArticle>(entity =>
            {
                // Настройка связи с назначенным пользователем
                entity.HasOne(a => a.AssignedUser)
                    .WithMany()
                    .HasForeignKey(a => a.AssignedUserId)
                    .OnDelete(DeleteBehavior.NoAction); // Явно указываем NO ACTION

                // Настройка связи с автором
                entity.HasOne(a => a.Author)
                    .WithMany()
                    .HasForeignKey(a => a.AuthorId)
                    .OnDelete(DeleteBehavior.NoAction);

                // Настройка связи с исходной заявкой
                entity.HasOne(a => a.SourceTicket)
                    .WithMany()
                    .HasForeignKey(a => a.SourceTicketId)
                    .OnDelete(DeleteBehavior.NoAction);
            });




            // Начальные данные
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Name = "Администратор",
                    Email = "admin@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345"), // В реальном приложении используйте хеширование
                    Role = "Admin",
                    Position = "Главный администратор",
                    Department = "IT",
                    IsActive = true,
                    //CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 2,
                    Username = "tech1",
                    Name = "Техник Иванов",
                    Email = "tech1@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345"),
                    Role = "TechSupport",
                    Position = "Технический специалист",
                    Department = "Поддержка",
                    IsActive = true,
                    //CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }

            );  
           
        } 
        
    }

}
