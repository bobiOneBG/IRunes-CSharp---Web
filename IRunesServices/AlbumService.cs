namespace IRunesServices
{
    using IRunes.Data;
    using IRunes.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AlbumService : IAlbumService
    {
        private readonly RunesDbContext context;

        public AlbumService()
        {
            this.context = new RunesDbContext();
        }

        public bool AddTrackToAlbum(string albumId, Track trackForDb)
        {
            Album albumFromDb = this.GetAlbumById(albumId);

            if (albumFromDb == null)
            {
                return false;
            }

            albumFromDb.Tracks.Add(trackForDb);
            albumFromDb.Price = (albumFromDb.Tracks
                                     .Select(track => track.Price)
                                     .Sum() * 87) / 100;

            this.context.Update(albumFromDb);
            this.context.SaveChanges();

            return true;
        }

        public Album CreateAlbum(Album album)
        {
                this.context.Albums.Add(album);
                this.context.SaveChanges();

            return album;
        }

        public Album GetAlbumById(string id)
        {
            return this.context.Albums
                .Include(album=>album.Tracks)
                .SingleOrDefault(a => a.Id == id);
        }

        public ICollection<Album> GetAllAlbums()
        {
            return this.context.Albums.ToList();
        }
    }
}