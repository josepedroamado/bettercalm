using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IPlaylistRepository
    {
        Playlist Get(int id);
        IEnumerable<Playlist> GetAll();
        IEnumerable<Playlist> GetAll(Category category);
    }
}