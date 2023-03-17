using DotNet_FinalProj.Context;
using DotNet_FinalProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_FinalProj.Repositories
{
    public class MovieRepository : GenerateRepository<Movie>
    {
        public MovieRepository() : base(new CinemaContext())
        {
        }
    }
}
