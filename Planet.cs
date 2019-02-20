using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Open
{
    class Planet : IPlanet
    {
        public double R { get; }
        public Vector3 Pos { get; set; }
        public PrimitiveType Type { get; set; }
        public Bitmap Bitmap { get; set; }
        public BitmapData BitmapData { get; set; }
        public double Angle { get; set; } 
        private  double Angle2;
        private double speed;
        private int textureId;
        public String Name { get; set; }


        public Planet(double r, Vector3 pos,double speed = 0, PrimitiveType type = PrimitiveType.QuadStrip)
        {
            this.R = r;
            this.Type = type;
            this.Pos = pos;
            this.speed = speed;
        }

        public void CreateBitMap(Bitmap bitmap)
        {
            this.Bitmap = bitmap;
            var Rect = new Rectangle(0, 0, Bitmap.Width, Bitmap.Height);
            BitmapData = Bitmap.LockBits(Rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }

       

        public void CreateSphere(int nx = 32, int ny = 32)
        {
            int ix, iy;
            double x, y, z, sy, cy, sy1, cy1, sx, cx, piy, pix, ay, ay1, ax, tx, ty, ty1, dnx, dny, diy;
            dnx = 1.0 / (double) nx;
            dny = 1.0 / (double) ny;
            // Рисуем полигональную модель сферы, формируем нормали и задаем коодинаты текстуры
            // Каждый полигон - это трапеция. Трапеции верхнего и нижнего слоев вырождаются в треугольники
            piy = Math.PI * dny;
            pix = Math.PI * dnx;
            for (iy = 0; iy < ny; iy++)
            {
                diy = (double) iy;
                ay = diy * piy;
                sy = Math.Sin(ay);
                cy = Math.Cos(ay);
                ty = diy * dny;
                ay1 = ay + piy;
                sy1 = Math.Sin(ay1);
                cy1 = Math.Cos(ay1);
                ty1 = ty + dny;
                for (ix = 0; ix <= nx; ix++)
                {
                    ax = 2.0 * ix * pix;
                    sx = Math.Sin(ax);
                    cx = Math.Cos(ax);
                    x = R * sy * cx;
                    y = R * sy * sx;
                    z = R * cy;
                    tx = (double) ix * dnx;
                    // Координаты нормали в текущей вершине
                    GL.Normal3(x, y, z); // Нормаль направлена от центра
                    // Координаты текстуры в текущей вершине
                    GL.TexCoord2(tx, ty);
                    GL.Vertex3(x, y, z);
                    x = R * sy1 * cx;
                    y = R * sy1 * sx;
                    z = R * cy1;
                    GL.Normal3(x, y, z);
                    GL.TexCoord2(tx, ty1);
                    GL.Vertex3(x, y, z);
                }
            }

        }

        public void CreateTexture()
        {
            // Активизируем режим вывода текстуры
            GL.Enable(EnableCap.Texture2D);
            // Генерируем идентификатор текстуры
            GL.GenTextures(0, out textureId);
            // Связываем текстуру с идентификатором
            GL.BindTexture(TextureTarget.Texture2D, textureId);
            // Параметры текстуры
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Linear);
            // Создаем текстуру
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Bitmap.Width, Bitmap.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, BitmapData.Scan0);
        }

        public void MoveAround(double Speed, double x = 0, double y = 0, double z = 0)
        {

            GL.Rotate(Angle2, x, y, z);
            //GL.Translate(0, (float)Math.Sin(Angle) * 42.0f, 220.0f * (float)Math.Cos(Angle));

            Angle2 += Speed;
            if (Angle2 > 360)
            {
                Angle2 -= 360;
            }
        }

        public void Move(double x = 0, double y = 0, double z = 0)
        {
            if (Name.Equals(Constant.Sun))
            {
                GL.Rotate(Angle, x, y, z);
            }
            //GL.Translate(0, (float)Math.Sin(Angle) * 42.0f, 220.0f * (float)Math.Cos(Angle));

            Angle += speed;
            if (Angle > 360)
            {
                Angle -= 360;
            }
        }
    }
}
