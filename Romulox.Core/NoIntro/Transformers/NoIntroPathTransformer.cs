using System.IO;

namespace Romulox.Core.NoIntro.Transformers
{
    public class NoIntroPathTransformer
    {
        public string Transform(string path)
        {
            NoIntroStringTransformer noIntroStringTransformer = new NoIntroStringTransformer();

            string fileName = Path.GetFileNameWithoutExtension(path);

            return noIntroStringTransformer.Transform(fileName);
        }
    }
}