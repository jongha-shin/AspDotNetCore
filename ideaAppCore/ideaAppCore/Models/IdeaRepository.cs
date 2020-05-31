using ideaAppCore.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ideaAppCore.Models
{
    public class IdeaRepository : IIdeaRepository
    {
        private readonly ApplicationDbContext _context;

        public IdeaRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        //입력
        public async Task<Idea> AddIdea(Idea idea)
        {
            _context.Ideas.Add(idea);
            await _context.SaveChangesAsync();
            return idea;
        }
        //출력
        public async Task<List<Idea>> GetIdeas()
        {
            return await _context.Ideas.ToListAsync();
        } 
    }
}
