using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contracts
{
    public interface ISpeakerPersist
    {
        //SPEAKERS
        Task<Speaker[]> GetAllSpeakersByNameAsync(string Name, bool IncludeEvents);
        Task<Speaker[]> GetAllSpeakersAsync(bool IncludeEvents);
        Task<Speaker> GetSpeakerByIdAsync(int SpeakerId, bool IncludeEvents);
    }
}