using AuthenticationProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationProject.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public virtual DbSet<Tenant> Tenants { get; set; }



    public virtual DbSet<Category> Categories { get; set; }





}


public class ApplicationDbCOntextFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly ICryptographyService _cryptographyService;
    private readonly ApplicationDbContext _context;

    public ApplicationDbCOntextFactory(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ICryptographyService cryptographyService, ApplicationDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _cryptographyService = cryptographyService;
        _context = context;
    }



    public ApplicationDbContext CreateContext()
    {
        var tenantId = _httpContextAccessor.HttpContext.Request.Headers["tenantId"].FirstOrDefault();

        var tId = _httpContextAccessor.HttpContext.User;

        string connectionString =default;



        if (!string.IsNullOrEmpty(tenantId))
        {
            connectionString = _configuration.GetConnectionString("default");

            
        }
        else
        {
            var tenant = _context.Tenants.FirstOrDefault(t => t.Ten == int.Parse(tenantId));
            connectionString = _cryptographyService.Decrypt(tenant.ConnectionString, tenant.TenancyName);
        }

        return default;
    }
}


