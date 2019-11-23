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
         public static Dictionary<Segment, Color> colors = new Dictionary<Segment, Color>();
 
         public static void SetColor(this Segment segment, Color color)
         {
             if (colors.ContainsKey(segment))
                 colors[segment] = color;
             else
                 colors.Add(segment, color);
         }
 
         public static Color GetColor(this Segment segment)
         {
             return
                 colors.TryGetValue(segment, out var value)
                     ? value
                     : new Color();
         }
     }
 }