using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceTracker.Models.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<Aluno>> GetAllAsync();
        Task<Aluno> GetByIdAsync(int id);
        Task<Aluno> CreateAsync(Aluno aluno);
        Task<bool> UpdateAsync(int id, Aluno aluno);
        Task<bool> DeleteAsync(int id);
    }
}