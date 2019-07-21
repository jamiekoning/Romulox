using System.Collections.Generic;

namespace Romulox.Core.GiantBomb.Entities
{ 
    /*
    * Set of classes used in a response from GiantBomb's /api/game/ Endpoint
    */
    
    public class GameResponse
    {
        public string Error { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int NumberOfPageResults { get; set; }
        public int NumberOfTotalResults { get; set; }
        public int StatusCode { get; set; }
        public Results Results { get; set; }
        public string Version { get; set; }
    }
    

    public class ImageTag
    {
        public string ApiDetailUrl { get; set; }
        public string Name { get; set; }
        public int Total { get; set; }
    }

    public class OriginalGameRating
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Platform
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
        public string Abbreviation { get; set; }
    }

    public class Image2
    {
        public string IconUrl { get; set; }
        public string MediumUrl { get; set; }
        public string ScreenUrl { get; set; }
        public string SmallUrl { get; set; }
        public string SuperUrl { get; set; }
        public string ThumbUrl { get; set; }
        public string TinyUrl { get; set; }
        public string Original { get; set; }
        public string Tags { get; set; }
    }

    public class Video
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class Character
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class Concept
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class FirstAppearanceCharacter
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class FirstAppearanceConcept
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class FirstAppearanceLocation
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class FirstAppearanceObject
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class FirstAppearancePeople
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class Franchise
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class Genre
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class Location
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class Object
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class Person
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class Publisher
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class Release
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class Review
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class SimilarGame
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class Theme
    {
        public string ApiDetailUrl { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteDetailUrl { get; set; }
    }

    public class Results
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
        public List<Image2> Images { get; set; }
        public List<Video> Videos { get; set; }
        public List<Character> Characters { get; set; }
        public List<Concept> Concepts { get; set; }
        public List<Developer> Developers { get; set; }
        public List<FirstAppearanceCharacter> FirstAppearanceCharacters { get; set; }
        public List<FirstAppearanceConcept> FirstAppearanceConcepts { get; set; }
        public List<FirstAppearanceLocation> FirstAppearanceLocations { get; set; }
        public List<FirstAppearanceObject> FirstAppearanceObjects { get; set; }
        public List<FirstAppearancePeople> FirstAppearancePeople { get; set; }
        public List<Franchise> Franchises { get; set; }
        public List<Genre> Genres { get; set; }
        public object KilledCharacters { get; set; }
        public List<Location> Locations { get; set; }
        public List<Object> Objects { get; set; }
        public List<Person> People { get; set; }
        public List<Publisher> Publishers { get; set; }
        public List<Release> Releases { get; set; }
        public List<Review> Reviews { get; set; }
        public List<SimilarGame> SimilarGames { get; set; }
        public List<Theme> Themes { get; set; }
    }
}