using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnEase.Repository.IRepository;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repository.Repository
{
    public class FlashcardRepository : GenericRepository<Course>, IFlashcardRepository
    {
        private readonly ApplicationDbContext _context;
        public FlashcardRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

    }
}
