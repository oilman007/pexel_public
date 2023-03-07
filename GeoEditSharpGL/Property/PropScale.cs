using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace Pexel
{
    public class PropScale
    {
        public PropScale()
        {
            Min = 0.0;
            Max = 0.0;
            Auto = true;
            Step = 100;
        }
        public PropScale(double min, double max, bool auto = true)
        {
            Min = min;
            Max = max;
            Auto = auto;
            Step = 100;
        }
        public double Min { set; get; }
        public double Max { set; get; }
        public bool Auto { set; get; }
        public double[] Histogram { set; get; }
        public int Step { set; get; }






        public void Write(BinaryWriter writer)
        {
            writer.Write(Min);
            writer.Write(Max);
            writer.Write(Auto);
        }



        public static PropScale Read(BinaryReader reader)
        {
            return new PropScale(reader.ReadDouble(), reader.ReadDouble(), reader.ReadBoolean());
        }




        public Color Color(double value)
        {
            /*
            if (Min == Max)
                return Color.Red;
            double temp = (value - Min) / (Max - Min) * 255.0f;
            */
            if (Min == Max)
                return HSV(120, 1.0, 1.0);
            double hue = (Max - Bound(Min, value, Max)) / (Max - Min) * 240;
            return HSV(hue, 1.0, 1.0);
        }


        double Bound(double min, double value, double max)
        {
            if      (value < min) return min;
            else if (value > max) return max;
            else                  return value;
        }


        int R(double value)
        {
            int r = 0;
            if (value < 63.0)
            {
                r = 0;
            }
            else if (value < 127.0)
            {
                r = 0;
            }
            else if (value < 191.0)
            {
                r = (int)value;
            }
            else
            {
                r = 255;
            }
            return r;
        }

        int G(double value)
        {
            int r = 0;
            if (value < 63.0)
            {
                r = (int)value;
            }
            else if (value < 127.0)
            {
                r = 255;
            }
            else if (value < 191.0)
            {
                r = 255;
            }
            else
            {
                r = 255 - (int)value;
            }
            return r;
        }

        int B(double value)
        {
            int r = 0;
            if (value < 63.0)
            {
                r = 255;
            }
            else if (value < 127.0)
            {
                r = 255 - (int)value;
            }
            else if (value < 191.0)
            {
                r = 0;
            }
            else
            {
                r = 0;
            }
            return r;
        }



        /// <summary>
        /// Convert HSV to RGB
        /// h is from 0-360
        /// s,v values are 0-1
        /// r,g,b values are 0-255
        /// Based upon http://ilab.usc.edu/wiki/index.php/HSV_And_H2SV_Color_Space#HSV_Transformation_C_.2F_C.2B.2B_Code_2
        /// </summary>
        void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
        {
            // ######################################################################
            // T. Nathan Mundhenk
            // mundhenk@usc.edu
            // C/C++ Macro HSV to RGB

            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((int)(R * 255.0));
            g = Clamp((int)(G * 255.0));
            b = Clamp((int)(B * 255.0));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }



        Color HSV(double h, double S, double V)
        {
            // ######################################################################
            // T. Nathan Mundhenk
            // mundhenk@usc.edu
            // C/C++ Macro HSV to RGB

            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            int r = Clamp((int)(R * 255.0));
            int g = Clamp((int)(G * 255.0));
            int b = Clamp((int)(B * 255.0));
            return System.Drawing.Color.FromArgb(r, g, b);
        }




    }
}
