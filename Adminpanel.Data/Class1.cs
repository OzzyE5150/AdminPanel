using Adminpanel.Data.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Adminpanel.Data;
public class AdminDbContext : IdentityDbContext
{
    public AdminDbContext() { }

    public AdminDbContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<CustomerAddress> CustomerAdresses { get; set; }
}
