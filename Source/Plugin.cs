using AForge;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static BioImager.Plugin;

namespace BioImager
{
    public class Plugin
    {
        public static Dictionary<string, IPlugin> Plugins = new Dictionary<string, IPlugin>();
        public interface IPlugin
        {
            string Name { get; }
            string MenuPath { get; }
            bool ContextMenu { get; }
            void Execute(string[] args);
            void KeyUpEvent(object o, KeyEventArgs e);
            void KeyDownEvent(object o, KeyEventArgs e);
            void KeyPressEvent(object o, KeyPressEventArgs e);
            void ScrollEvent(object o, MouseEventArgs args);
            void Paint(object o, PaintEventArgs e);
            void Render(object o, Direct2D dx);
            void MouseMove(object o, PointD e, MouseEventArgs buts);
            void MouseUp(object o, PointD e, MouseEventArgs buts);
            void MouseDown(object o, PointD e, MouseEventArgs buts);
        }
    }
    
    public static class Plugins
    {
        public static void Initialize()
        {
            foreach (string s in Directory.GetFiles("Plugins"))
            {
                if (!s.EndsWith(".dll") || Plugin.Plugins.ContainsKey(Path.GetFileName(s)))
                    continue;
                // Load the plugin assembly
                Assembly pluginAssembly = Assembly.LoadFile(Environment.CurrentDirectory + "/" + s);
                // Find the type which implements the IPlugin interface
                var pluginType = pluginAssembly.GetTypes().FirstOrDefault(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface);
                if (pluginType != null)
                {
                    // Create an instance of this type
                    IPlugin pluginInstance = Activator.CreateInstance(pluginType) as IPlugin;
                    Plugin.Plugins.Add(Path.GetFileName(s), pluginInstance);
                    if (pluginInstance.ContextMenu)
                        App.AddContextMenu(pluginInstance.MenuPath);
                    else
                        App.AddMenu(pluginInstance.MenuPath);                   
                }
                else
                {
                    Console.WriteLine("No plugin found in the assembly: " + s);
                }
            }
        }
        public static void KeyUpEvent(object o, KeyEventArgs e)
        {
            foreach (IPlugin p in Plugin.Plugins.Values)
            {
                p.KeyUpEvent(o, e);
            }
        }
        public static void KeyDownEvent(object o, KeyEventArgs e)
        {
            foreach (IPlugin p in Plugin.Plugins.Values)
            {
                p.KeyDownEvent(o, e);
            }
        }
        public static void KeyPressEvent(object o, KeyPressEventArgs e)
        {
            foreach (IPlugin p in Plugin.Plugins.Values)
            {
                p.KeyPressEvent(o, e);
            }
        }
        public static void ScrollEvent(object o, MouseEventArgs args)
        {
            foreach (IPlugin p in Plugin.Plugins.Values)
            {
                p.ScrollEvent(o, args);
            }
        }
        public static void Paint(object o, PaintEventArgs e)
        {
            foreach (IPlugin p in Plugin.Plugins.Values)
            {
                p.Paint(o, e);
            }
        }
        public static void Render(object o, Direct2D dx)
        {
            foreach (IPlugin p in Plugin.Plugins.Values)
            {
                p.Render(o, dx);
            }
        }
        public static void MouseMove(object o, PointD e, MouseEventArgs buts)
        {
            foreach (IPlugin p in Plugin.Plugins.Values)
            {
                p.MouseMove(o, e, buts);
            }
        }
        public static void MouseUp(object o, PointD e, MouseEventArgs buts)
        {
            foreach (IPlugin p in Plugin.Plugins.Values)
            {
                p.MouseUp(o, e, buts);
            }
        }
        public static void MouseDown(object o, PointD e, MouseEventArgs buts)
        {
            foreach (IPlugin p in Plugin.Plugins.Values)
            {
                p.MouseDown(o, e, buts);
            }
        }
    }

}
