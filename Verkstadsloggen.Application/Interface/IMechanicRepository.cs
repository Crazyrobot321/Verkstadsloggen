using Verkstadsloggen.Domain.Models;

namespace Verkstadsloggen.Application.Interfaces
{
    public interface IMechanicRepository
    {
        Task<List<Mechanic>> GetAllMechanicsAsync();
    }
}
