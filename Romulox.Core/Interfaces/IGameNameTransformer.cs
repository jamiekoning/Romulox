namespace Romulox.Core.Interfaces
{
    /* Transforms a file name to a Game Title for API Searching */
    public interface IGameNameTransformer
    {
        string Transform(string fileName);
    }
}