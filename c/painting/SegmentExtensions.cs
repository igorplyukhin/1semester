using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GeometryTasks;

namespace GeometryPainting
{
    /*public static class SegmentExtentions
    {
        public class Wraper
        {
            public Color WrapedColor;
        }
        public static ConditionalWeakTable<Segment, Wraper> colors = new ConditionalWeakTable<Segment, Wraper>();

        public static void SetColor(this Segment segment, Color color)
        {
            var wraper = new Wraper{WrapedColor = color};
            colors.Add(segment, wraper);
        }

        public static Color GetColor(this Segment segment) =>
            colors.TryGetValue(segment, out var value)
                ? value.WrapedColor
                : new Color();
    }*/
    
    public static class SegmentExtentions
    {
        public static Dictionary<Segment, Color> colors = new Dictionary<Segment, Color>();

        public static void SetColor(this Segment segment, Color color) =>
            colors[segment] = color;

        public static Color GetColor(this Segment segment) =>
            colors.TryGetValue(segment, out var value)
                ? value
                : new Color();
    }
}
