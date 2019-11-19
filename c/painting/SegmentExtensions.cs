using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryTasks;

namespace GeometryPainting
{
    public static class SegmentExtentions
    {
        public static Dictionary<Segment, Color> Colors = new Dictionary<Segment, Color>();

        public static void SetColor(this Segment segment, Color color)
        {
            if (Colors.ContainsKey(segment))
                Colors[segment] = color;
            else
                Colors.Add(segment, color);
        }

        public static Color GetColor(this Segment segment)
        {
            return
                Colors.ContainsKey(segment) 
                    ? Colors[segment]
                    : new Color();
        }
    }
}