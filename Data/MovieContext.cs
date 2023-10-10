using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using parcialherramientas.Models;

namespace parcialherramientas.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext (DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<parcialherramientas.Models.Movie> MovieDbSet { get; set; } = default!;

        public DbSet<parcialherramientas.Models.Actor> ActorDbSet { get; set; } = default!;
    }
}
