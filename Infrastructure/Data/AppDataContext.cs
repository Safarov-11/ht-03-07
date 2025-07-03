using Domain.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDataContext(DbContextOptions<AppDataContext> options) : IdentityDbContext(options)
{
    public DbSet<Product> Products { get; set; }
}
    