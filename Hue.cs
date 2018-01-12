using System.IO;
using System.Drawing;
using MusicBeePlugin;
using ColorThiefDotNet;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MusicBeePlugin
{

    public class Hue
    {
        /// <summary>
        /// Retrieve the album art from a given file url
        /// </summary>
        public Image getAlbumArt(string path)
        {
            try
            {

                var file = TagLib.File.Create(path);
                if (file.Tag.Pictures.Length >= 1)
                {
                    var bin = (byte[])(file.Tag.Pictures[0].Data.Data);
                    MemoryStream ms = new MemoryStream(bin);
                    Image albumArt = Image.FromStream(ms);
                    return albumArt;
                }
                else
                {
                    var path2 = Path.GetDirectoryName(path);
                    Bitmap bitmap;
                    using (Stream bmpStream = System.IO.File.Open(path2 + @"\cover.jpg", System.IO.FileMode.Open))
                    {
                        Image image = Image.FromStream(bmpStream);

                        bitmap = new Bitmap(image);

                    }
                    return bitmap;
                }

            }
            catch (Exception e)
            {
                //if (e is TagLib.UnsupportedFormatException || e is FileNotFoundException)
                //{
                var blank = new Bitmap(8, 8);
                blank.SetPixel(0, 0, System.Drawing.Color.White);
                return blank;
                //}

            }

        }

        /// <summary>
        /// Get the brightness value based on the image given
        /// </summary>
        public double getBrightness(Bitmap albumArt)
        {
            double rAverage = 0;
            double gAverage = 0;
            double bAverage = 0;
            int counter = 0;
            double brightness = 0;
            for (int i = 0; i < albumArt.Width; i += 10)
            {
                for (int j = 0; j < albumArt.Height; j += 10)
                {
                    System.Drawing.Color pixel = albumArt.GetPixel(i, j);
                    rAverage += pixel.R;
                    gAverage += pixel.G;
                    bAverage += pixel.B;

                    counter++;
                }
            }

            int red = (int)rAverage / counter;
            int green = (int)gAverage / counter;
            int blue = (int)bAverage / counter;

            brightness = (0.299 * (red) + 0.587 * (green) + 0.114 * (blue));

            return brightness;
        }

        /// <summary>
        /// Get the RGB average for a given picture
        /// </summary>
        public List<int> getRGBAverage(Bitmap albumArt, int resizeFactor)
        {

            if (albumArt == null)
            {
                return new List<int> { 255, 255, 255 };
            }

            double rAverage = 0;
            double gAverage = 0;
            double bAverage = 0;
            int counter = 0;

            using (var resizedArt = ResizeBitmap(albumArt, albumArt.Width * (resizeFactor / 100), albumArt.Height * (resizeFactor / 100)))
            {
                for (int i = 0; i < albumArt.Width; i += 10)
                {
                    for (int j = 0; j < albumArt.Height; j += 10)
                    {
                        System.Drawing.Color pixel = albumArt.GetPixel(i, j);
                        rAverage += pixel.R;
                        gAverage += pixel.G;
                        bAverage += pixel.B;
                        counter++;
                    }
                }
            }


            int red = (int)rAverage / counter;
            int green = (int)gAverage / counter;
            int blue = (int)bAverage / counter;

            return new List<int> { red, green, blue };
        }

        /// <summary>
        /// Convert RGB to XY as required for the Philips Hue Bulb
        /// </summary>
        public List<float> ToXYZ(int r, int g, int b)
        {
            float rF = r / 255f;
            float gF = g / 255f;
            float bF = b / 255f;

            float red = (rF > 0.04045f) ? (float)Math.Pow((rF + 0.055f) / (1.0f + 0.055f), 2.4f) : (rF / 12.92f);
            float green = (gF > 0.04045f) ? (float)Math.Pow((gF + 0.055f) / (1.0f + 0.055f), 2.4f) : (gF / 12.92f);
            float blue = (bF > 0.04045f) ? (float)Math.Pow((bF + 0.055f) / (1.0f + 0.055f), 2.4f) : (bF / 12.92f);

            float X = red * 0.664511f + green * 0.154324f + blue * 0.162028f;
            float Y = red * 0.283881f + green * 0.668433f + blue * 0.047685f;
            float Z = red * 0.000088f + green * 0.072310f + blue * 0.986039f;

            float x = X / (X + Y + Z);
            float y = Y / (X + Y + Z);

            return new List<float> { x, y };

        }

        /// <summary>
        /// Get a color palette based on the image given.
        /// <paramref name="albumArt"/> Bitmap of the image</param>
        /// <paramref name="paletteSize"/> Amount of different colors. eg. Color palette size of 8 = 8 colors</param>
        /// <paramref name="resizeFactor"/> Resize image factor. eg resize factor 50 sizes image down by half</param>
        /// </summary>
        public List<string> getColorPalette(Bitmap albumArt, int paletteSize, int resizeFactor)
        {
            List<string> colorMajorities = new List<string>();


            if (albumArt == null)
            {
                return new List<string> { "FFFFFF" };
            }

            using (var resizedArt = ResizeBitmap(albumArt, albumArt.Width * (resizeFactor / 100), albumArt.Height * (resizeFactor / 100)))
            {
                var colorThief = new ColorThief();
                var palette = colorThief.GetPalette(resizedArt, paletteSize).ToList();
                foreach (QuantizedColor i in palette)
                {
                    colorMajorities.Add(i.Color.R.ToString("X2") + i.Color.G.ToString("X2") + i.Color.B.ToString("X2"));

                }

            }

            return colorMajorities;

        }

        /// <summary>
        /// Resize a bitmap image
        /// </summary>
        public Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(sourceBMP, 0, 0, width, height);

            }
            return result;
        }

        /// <summary>
        /// Get the red component from a RGB Hex String ex. getRed("A2B4CC")
        /// </summary>
        public int getRed(string hexString)
        {
            return int.Parse(hexString.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>
        /// Get the green component from a RGB Hex String ex. getGreen("A2B4CC")
        /// </summary>
        public int getGreen(string hexString)
        {
            return int.Parse(hexString.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>
        /// Get the blue component from a RGB Hex String ex. getBlue("A2B4CC")
        /// </summary>
        public int getBlue(string hexString)
        {
            return int.Parse(hexString.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        }


        /// <summary>
        /// Get the percieved Hue from an RGB value
        /// </summary>
        public int getHue(int red, int green, int blue)
        {

            float min = Math.Min(Math.Min(red, green), blue);
            float max = Math.Max(Math.Max(red, green), blue);

            if (min == max)
            {
                return 0;
            }

            float hue = 0f;
            if (max == red)
            {
                hue = (green - blue) / (max - min);

            }
            else if (max == green)
            {
                hue = 2f + (blue - red) / (max - min);

            }
            else
            {
                hue = 4f + (red - green) / (max - min);
            }

            hue = hue * 60;
            if (hue < 0) hue = hue + 360;

            return (int)hue;
        }


    }



}
