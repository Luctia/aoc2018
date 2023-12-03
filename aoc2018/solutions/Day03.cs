using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc2018.solutions
{
    internal class Day03 : Day
    {
        public override void Part1()
        {
            var bitmap = GetBitmap();
            int overlappingLayers = 0;
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    if (bitmap.GetPixel(x, y).R > 127)
                    {
                        overlappingLayers++;
                    }
                }
            }
            Answer(overlappingLayers);
        }

        public override void Part2()
        {
            var bitmap = GetBitmap();
            var claims = GetClaims();
            foreach (var claim in claims)
            {
                foreach (var point in claim.GetAllPoints())
                {
                    var pixel = bitmap.GetPixel(point.X, point.Y);
                    if (pixel.R > 127)
                    {
                        goto NewLoop;
                    }
                }
                Answer(claim.id);
                return;
                NewLoop:;
            }
        }

        private Bitmap GetBitmap(string version = null)
        {
            var claims = GetClaims(version);
            var requiredSize =
                new Size(claims.Max(c => c.RightBottom.X), claims.Max(c => c.RightBottom.Y));
            var bitmap = new Bitmap(requiredSize.Width, requiredSize.Height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, bitmap.Width, bitmap.Height);
            foreach (var claim in claims)
                claim.Draw(graphics);
            // bitmap.Save("output.bmp", ImageFormat.Png);
            return bitmap;
        }

        private Claim[] GetClaims(string version = null)
        {
            string[] claimLines = GetInputLines(version);
            return claimLines.Select(line => new Claim(line)).ToArray();

        }

        private class Claim
        {
            public int id;
            Point leftTop;
            Size size;

            static readonly Brush brush = new SolidBrush(Color.FromArgb(127, Color.White));

            public Claim(string claim)
            {
                string[] parts = claim.Split(new[] { "#", " @ ", ",", ": ", "x" }, StringSplitOptions.RemoveEmptyEntries);
                this.id = int.Parse(parts[0]);
                this.leftTop = new Point(int.Parse(parts[1]), int.Parse(parts[2]));
                this.size = new Size(int.Parse(parts[3]), int.Parse(parts[4]));
                RightBottom = new Point(leftTop.X + size.Width, leftTop.Y + size.Height);
            }

            public Point RightBottom { get; }

            public Point[] GetAllPoints()
            {
                List<Point> points = new List<Point>();
                for (int x = 0; x < size.Width; x++)
                {
                    for (int y = 0; y < size.Height; y++)
                    {
                        points.Add(new Point(this.leftTop.X + x, this.leftTop.Y + y));
                    }
                }
                return points.ToArray();
            }

            public void Draw(Graphics g)
            {
                g.FillRectangle(brush, leftTop.X, leftTop.Y, size.Width, size.Height);
            }
        }
    }
}
