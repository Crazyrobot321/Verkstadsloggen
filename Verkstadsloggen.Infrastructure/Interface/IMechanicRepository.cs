using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Infrastructure.Interface
{
    public interface IMechanicRepository
    {
        Task<List<Mechanic>> GetAllMechanicsAsync();
    }
}