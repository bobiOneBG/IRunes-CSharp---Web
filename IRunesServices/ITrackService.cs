namespace IRunesServices
{
    using IRunes.Models;

    public interface ITrackService
    {
        Track CreateTrack(Track track);

        Track GetTrackById(string trackId);
    }
}