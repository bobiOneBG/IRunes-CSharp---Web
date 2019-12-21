namespace IRunesServices
{
    using IRunes.Models;
    using System.Collections.Generic;

    public interface IAlbumService
    {
        Album CreateAlbum(Album album);

        bool AddTrackToAlbum(string albumId, Track track);

        ICollection<Album> GetAllAlbums();

        Album GetAlbumById(string id);
    }
}