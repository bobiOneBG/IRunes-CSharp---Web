namespace IRunesServices
{
    using IRunes.Data;
    using IRunes.Models;
    using System;
    using System.Linq;

    public class TrackService : ITrackService
    {
        private readonly RunesDbContext context;

        public TrackService()
        {
            this.context = new RunesDbContext();
        }
        public Track CreateTrack(Track track)
        {
            track = this.context.Tracks.Add(track).Entity;
            this.context.SaveChanges();

            return track;
        }

        public Track GetTrackById(string trackId)
        {
            return context.Tracks.SingleOrDefault(track => track.Id == trackId);
        }
    }
}