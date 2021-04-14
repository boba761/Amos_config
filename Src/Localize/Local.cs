using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Localize
{
    public class Local
    {
        private static ResourceManager _resourceManager;

        private static ResourceManager ResourceManager
        {
            get
            {
                if (_resourceManager == null)
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    string baseName = assembly.GetName().Name + ".locale";
                    _resourceManager = new ResourceManager(baseName, assembly);
                }
                return _resourceManager;
            }
        }

        public static string GetString(string id)
        {
            return ResourceManager.GetString(id, Thread.CurrentThread.CurrentCulture);
        }

        public static Bitmap GetImage(string id)
        {
            return (Bitmap)ResourceManager.GetObject("image." + id, Thread.CurrentThread.CurrentCulture);
        }

        public static Icon GetIcon(string id)
        {
            return (Icon)ResourceManager.GetObject("icon." + id, Thread.CurrentThread.CurrentCulture);
        }

        public static Cursor GetCursor( string id )
        {
            return new Cursor(new MemoryStream((byte[])ResourceManager.GetObject( "cur." + id, Thread.CurrentThread.CurrentCulture )));
        }

        public static void LocalizeObject(object obj)
        {
            if (obj is Control)
            {
                LocalizeControl(obj as Control);
                if (obj is ToolStrip)
                    LocalizeToolStrip(obj as ToolStrip);
                else if (obj is ContainerControl)
                    LocalizeContainerControl(obj as ContainerControl);
                else if (obj is Panel)
                    LocalizePanel(obj as Panel);
                else if (obj is TabControl)
                    LocalizeTabControl(obj as TabControl);
            }
            else if (obj is ToolStripItem)
            {
                LocalizeToolStripItem(obj as ToolStripItem);
                if (obj is ToolStripDropDownItem)
                    LocalizeToolStripDropDownItem(obj as ToolStripDropDownItem);
            }
            else
                Debug.WriteLine(string.Format("Not type - {0} : {1}", obj.GetType().Name, obj));
        }

        private static void LocalizeControl(Control item)
        {
            string id = string.Format("{0}.{1}", item.AccessibleName,
                Enum.GetName(typeof(AccessibleRole), item.AccessibleRole));
            switch (item.AccessibleRole)
            {
                case AccessibleRole.Window:
                    item.Text = GetString(id);
                    if (item is Form)
                        (item as Form).Icon = GetIcon(id);
                    break;
                case AccessibleRole.PageTab:
                case AccessibleRole.MenuBar:
                case AccessibleRole.ToolBar:
                case AccessibleRole.CheckButton:
                case AccessibleRole.Text:
                    item.Text = GetString(id);
                    break;
                case AccessibleRole.PushButton:
                    item.Text = GetString(id);
                    if (item is Button)
                        (item as Button).Image = GetImage(id);
                    break;
                case AccessibleRole.Graphic:
                    if (item is PictureBox)
                        (item as PictureBox).Image = GetImage(id);
                    break;
                default:
                    //Debug.WriteLine(item.Name + " - " + item);
                    break;
            }
        }

        private static void LocalizeContainerControl(ContainerControl item)
        {
            foreach (object obj in item.Controls)
                LocalizeObject(obj);
        }

        private static void LocalizePanel(Panel item)
        {
            foreach (object obj in item.Controls)
                LocalizeObject(obj);
        }

        private static void LocalizeTabControl(TabControl item)
        {
            foreach (object obj in item.TabPages)
                LocalizeObject(obj);
        }

        private static void LocalizeToolStrip(ToolStrip item)
        {
            foreach (object obj in item.Items)
                LocalizeObject(obj);
        }

        private static void LocalizeDockPanel(DockPanel item)
        {
            foreach (object obj in item.DockWindows)
                LocalizeObject(obj);
        }

        private static void LocalizeToolStripItem(ToolStripItem item)
        {
            if (item.AccessibleRole == AccessibleRole.Default)
                return;
            string id = string.Format("{0}.{1}", item.AccessibleName,
                Enum.GetName(typeof(AccessibleRole), item.AccessibleRole));
            item.Text = GetString(id);
            item.ToolTipText = GetString(item.AccessibleName + ".ToolTip");
            item.Image = GetImage(id);
        }

        private static void LocalizeToolStripDropDownItem(ToolStripDropDownItem item)
        {
            foreach (object obj in item.DropDownItems)
                LocalizeObject(obj);
        }
    }
}
