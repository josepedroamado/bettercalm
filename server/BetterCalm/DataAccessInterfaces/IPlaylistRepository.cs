using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IPlaylistRepository
    {
        IEnumerable<Playlist> GetAll();
        Playlist Get(int id);
    }
}