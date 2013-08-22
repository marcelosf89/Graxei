using System.Web.Hosting;
using System.Web.Caching;
using System.Collections;
using System;
using System.IO;
using System.Web;
using System.Reflection;

namespace FAST.Utils
{
    public class AssemblyResourceProvider : VirtualPathProvider
    {
        string _ResourcePrefix;
        string _dirDLL;
        Type _typeInterface;

        public AssemblyResourceProvider()
            : this("EmbeddedWebResource")
        {
        }

        public AssemblyResourceProvider(string prefix)
        {
            _ResourcePrefix = prefix;
        }

        public string DirDLL
        {
            get
            {
                return _dirDLL;
            }
            set
            {
                _dirDLL = value;
            }
        }

        public Type TypeInterface
        {
            get
            {
                return _typeInterface;
            }
            set
            {
                _typeInterface = value;
            }
        }

        private bool IsAppResourcePath(string virtualPath)
        {
            String checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
            return checkPath.StartsWith("~/" + _ResourcePrefix + "/",
                   StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool FileExists(string virtualPath)
        {
            return (IsAppResourcePath(virtualPath) ||
                    base.FileExists(virtualPath));
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsAppResourcePath(virtualPath))
            {
                if (String.IsNullOrEmpty(_dirDLL))
                {

                    return new AssemblyResourceVirtualFile(virtualPath, TypeInterface);
                }
                else
                {
                    return new AssemblyResourceVirtualFile(virtualPath, _dirDLL, TypeInterface);
                }
            }
            else
                return base.GetFile(virtualPath);
        }

        public override CacheDependency
               GetCacheDependency(string virtualPath,
               IEnumerable virtualPathDependencies,
               DateTime utcStart)
        {
            if (IsAppResourcePath(virtualPath))
                return null;
            else
                return base.GetCacheDependency(virtualPath,
                       virtualPathDependencies, utcStart);
        }
    }

    class AssemblyResourceVirtualFile : VirtualFile 
    {
        string path;
        string _dirDLL = HttpRuntime.BinDirectory;
        Type _typeInterface = null;

        public AssemblyResourceVirtualFile(string virtualPath, Type typeInterface)
            : base(virtualPath)
        {
            path = VirtualPathUtility.ToAppRelative(virtualPath);
            _typeInterface = typeInterface;
        }

        public AssemblyResourceVirtualFile(string virtualPath, string dirDLL, Type typeInterface)
            : this(virtualPath, typeInterface)
        {
            this._dirDLL = dirDLL;
        }

        public override Stream Open()
        {
            string[] parts = path.Split('/');
            string assemblyName = parts[2];
            string resourceName = parts[3];

            assemblyName = Path.Combine(_dirDLL, assemblyName);
            Assembly assembly = Assembly.LoadFile(assemblyName);
           
            if (assembly == null) throw new Exception("Failed to load " + assemblyName);
            if (_typeInterface != null)
            {
                Type tp = assembly.GetType(resourceName.Replace(".ascx",String.Empty)).GetInterface(_typeInterface.Name);
                if (tp == null) { throw new Exception("The assembly '" + assemblyName + "' does not implements the interface " + _typeInterface.Name); }
            }
            Stream s = assembly.GetManifestResourceStream(resourceName);
            if (s == null) throw new Exception("Failed to load " + resourceName);
            return s;
        }
    }
}

