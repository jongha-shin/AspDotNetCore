using System.Collections.Generic;
using System.Threading.Tasks;

namespace ideaAppCore.Models
{
    public interface IIdeaRepository
    {
        Task<Idea> AddIdea(Idea idea);
        Task<List<Idea>> GetIdeas();
    }
}
