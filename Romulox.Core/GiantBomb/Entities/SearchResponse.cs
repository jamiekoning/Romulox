using System.Collections.Generic;

namespace Romulox.Core.GiantBomb.Entities
{
    /*
     * Set of classes used in a response from GiantBomb's /api/search/ Endpoint
     */
    
    public class SearchResponse
    {
        public string Error { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int NumberOfPageResults { get; set; }
        public int NumberOfTotalResults { get; set; }
        public int StatusCode { get; set; }
        public List<Result> Results { get; set; }
        public string Version { get; set; }
    }

    public class Image
    {
        public string IconUrl { get; set; }
        public string MediumUrl { get; set; }
        public string ScreenUrl { get; set; }
        public string ScreenLargeUrl { get; set; }
        public string SmallUrl { get; set; }
        public string SuperUrl { get; set; }
        public string ThumbUrl { get; set; }
        public string TinyUrl { get; set; }
        public string OriginalUrl { get; set; }
        public string ImageTags { get; set; }
    }
    
    public class Association
    {
        public string ApiDetailUrl { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class VideoShow
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public string SiteDetailUrl { get; set; }
        public Image2 Image { get; set; }
    }

    public class VideoCategory
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class Result
    {
        public string Aliases { get; set; }
        public string ApiDetailUrl { get; set; }
        public string DateAdded { get; set; }
        public string DateLastUpdated { get; set; }
        public string Deck { get; set; }
        public string Description { get; set; }
        public int? ExpectedReleaseDay { get; set; }
        public int? ExpectedReleaseMonth { get; set; }
        public int? ExpectedReleaseQuarter { get; set; }
        public int? ExpectedReleaseYear { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public Image Image { get; set; }
        public List<ImageTag> ImageTags { get; set; }
        public string Name { get; set; }
        public int NumberOfUserReviews { get; set; }
        public List<OriginalGameRating> OriginalGameRating { get; set; }
        public string OriginalReleaseDate { get; set; }
        public List<Platform> Platforms { get; set; }
        public string SiteDetailUrl { get; set; }
        public string ResourceType { get; set; }
        public List<Association> Associations { get; set; }
        public string EmbedPlayer { get; set; }
        public int? LengthSeconds { get; set; }
        public bool? Premium { get; set; }
        public string PublishDate { get; set; }
        public string User { get; set; }
        public string VideoType { get; set; }
        public VideoShow VideoShow { get; set; }
        public List<VideoCategory> VideoCategories { get; set; }
        public object SavedTime { get; set; }
        public object YoutubeId { get; set; }
        public string HighUrl { get; set; }
        public object LowUrl { get; set; }
        public string Url { get; set; }
    }
}