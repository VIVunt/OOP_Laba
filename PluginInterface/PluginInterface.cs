using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Drawing2D;

namespace PluginInterface
{
    public interface IDraw
    {
        void Initialization(Point point, int thickness, Color front, Color back);
        void Draw(Graphics graph);
        void SetPoint(Point point);
        void AddPoint(Point point);
        bool IsSimpleFigure();
        void Save();
        int GetWidth();
        int GetHeight();
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PluginAttribute : Attribute
    {
        private string _Name;
        public PluginAttribute(string name) { _Name = name; }
        public string Name { get { return _Name; } }
    };
}
