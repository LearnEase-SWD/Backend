using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnEase.Core.Entities;
using LearnEase.Repository.IRepository;
using LearnEase_Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repository.Repository
{
    public class ExerciseRepository : GenericRepository<Exercise>, IExerciseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExerciseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
