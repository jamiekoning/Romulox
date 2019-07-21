using System;

namespace Romulox.Core.Helpers
{
    public class PathTools
    {
        public string AbsolutePathToWebRootPath(string absolutePath)    
        {
            const string wwwroot = "/wwwroot";
            var index = absolutePath.IndexOf(wwwroot, StringComparison.CurrentCulture);
            return absolutePath.Substring(index + wwwroot.Length);
        }
    }
}