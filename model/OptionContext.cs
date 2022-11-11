using Microsoft.EntityFrameworkCore;
using OptionEval.Model;

namespace OptionEval.Models;

public class OptionContext : DbContext
{
    public OptionContext(DbContextOptions<OptionContext> options): base(options)
    {
    }

    public DbSet<OptionRequest> TodoItems { get; set; } = null!;
}