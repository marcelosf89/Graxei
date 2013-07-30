using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FAST.Utils;

namespace FAST.Web.Controls
{
    [ToolboxData("<{0}:ControlUC runat=server></{0}:ControlUC>")]
    public class ControlUC : WebControl
    {
        # region VirtualPathProvider setup code
        static ControlUC()
        {
            if (!IsDesignMode)
            {
                System.Web.Hosting.HostingEnvironment.
                RegisterVirtualPathProvider(
            new AssemblyResourceProvider(ResourcePrefix));
            }
        }

        static bool IsDesignMode
        {
            get
            {
                return HttpContext.Current == null;
            }
        }

        static string mResourcePrefix = "EmbeddedWebResource";

        public static string ResourcePrefix
        {
            get
            {
                return mResourcePrefix;
            }

            set
            {
                mResourcePrefix = value;
            }
        }


        #endregion

        #region Toolbox properties
        string mDirectoryDll = String.Empty;

        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        public string DirectoryDll
        {
            get
            {
                return mDirectoryDll;
            }

            set
            {
                mDirectoryDll = value;
            }
        }


        Type _TypeInterface;

        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        public Type TypeInterface
        {
            get
            {
                return _TypeInterface;
            }

            set
            {
                _TypeInterface = value;
            }
        }

        private string mAssemblyName = "";

        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        public string AssemblyName
        {
            get { return mAssemblyName; }
            set { mAssemblyName = value; }
        }

        private string mControlNamespace = "";

        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        public string ControlNamespace
        {
            get { return mControlNamespace; }
            set { mControlNamespace = value; }
        }

        private string mControlClassName = "";

        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        public string ControlClassName
        {
            get { return mControlClassName; }
            set { mControlClassName = value; }
        }
        #endregion

        public Control ControlLoad
        {
            get { return c; }
        }

        #region Private members

        string Path
        {
            get
            {
                return String.Format("/{0}/{1}.dll/{2}.{3}.ascx",
            ResourcePrefix, AssemblyName, ControlNamespace,
            ControlClassName);
            }
        }

        Control c;


        protected override void OnLoad(EventArgs e)
        {
            Bind();
            base.OnLoad(e);
        }

        public void Bind()
        {
            ((AssemblyResourceProvider)System.Web.Hosting.HostingEnvironment.VirtualPathProvider).DirDLL = DirectoryDll;
            ((AssemblyResourceProvider)System.Web.Hosting.HostingEnvironment.VirtualPathProvider).TypeInterface = TypeInterface;
            try
            {
                c = Page.LoadControl(Path);
                Controls.Add(c);
            }
            catch (Exception)
            {
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            if (IsDesignMode)
            {
                output.Write(Path);
                return;
            }
            base.RenderContents(output);
        }
        #endregion

        #region Helper members to access UserControl properties

        public void SetControlProperty(string propName, object value)
        {
            c.GetType().GetProperty(propName).SetValue(c, value, null);
        }

        public object GetControlProperty(string propName)
        {
            return c.GetType().GetProperty(propName).GetValue(c, null);
        }
        #endregion
    }
}

