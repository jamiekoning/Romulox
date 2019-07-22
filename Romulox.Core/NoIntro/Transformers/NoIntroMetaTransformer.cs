using Romulox.Core.Interfaces;

namespace Romulox.Core.NoIntro.Transformers
{
    public class NoIntroMetaTransformer : IGameNameTransformer
    {
        private string datFilePath;

        public NoIntroMetaTransformer(string datFilePath)
        {
            this.datFilePath = datFilePath;
        }
        public string Transform(string path)
        {
            NoIntroStringTransformer noIntroStringTransformer = new NoIntroStringTransformer();
            
            if (datFilePath != null)
            {
                var gameName = new NoIntroHashTransformer(datFilePath).Transform(path);

                if (gameName != null)
                {
                    return noIntroStringTransformer.Transform(gameName);
                }
                
            }

            return noIntroStringTransformer.Transform(path);
        }
    }
}