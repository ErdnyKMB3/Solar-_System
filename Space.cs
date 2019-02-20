using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Open
{
    class Space
    {
        private int x = 0;
        private int y = 0;
        private int z = 0;
        private Dictionary<string, IPlanet> planets;

        private List<Planet> stars;

        bool showTexture = true;
        bool useMaterial = false;
        bool showNormals = false;
        bool flat = true;
        private GameWindow window;
        private double angle;
        private double angle2;
        private double angle3;
        public Space(GameWindow window)
        {
            this.window = window;
            Start();
        }

        private void Start()
        {
            window.Load += Loaded;
           // window.Resize += Resize;
            window.KeyPress += KeyPress;
            window.RenderFrame += Render;
            window.TargetRenderFrequency = 30;
            window.Run(30);
        }

        public void KeyPress(object sender, EventArgs e)
        {
            
            var ev = (KeyPressEventArgs) e; Console.WriteLine(ev.KeyChar);
            switch (ev.KeyChar)
            {
                case 'w':
                    Console.WriteLine(y);
                    y = y < 0 ? 0 : y + 1;
                    GL.Rotate(y, new Vector3d(0, 1, 0));
                    break;
                case 's':
                    Console.WriteLine(y);
                    y = y > 0 ? 0 : y - 1;
                    GL.Rotate(y, new Vector3d(0, 1, 0));
                    break;
                case 'a':
                    Console.WriteLine(z);
                    z = z < 0 ? 0 : z + 1;
                    GL.Rotate(z, new Vector3d(0, 0, 1));
                    break;
                case 'd':
                    Console.WriteLine(z);
                    z = z > 0 ? 0 : z - 1;
                    GL.Rotate(z, new Vector3d(0, 0, 1));
                    break;
            }
        }


        private void Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, window.Width, window.Height);
            GL.MatrixMode(MatrixMode.Projection);            
            GL.Ortho(-50.0, 50.0, -50.0, 50.0, -1.0, 1.0);
            //var matrix4 = Matrix4.CreatePerspectiveFieldOfView(45.0f, (float)window.Width / window.Height, 1.0f, 100.0f);
            //GL.LoadMatrix(ref matrix4);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }
        private void Render(object sender, EventArgs e)
        {
           GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Viewport(-10, 0, window.Width, window.Height);
            GL.Color3(Color.LightGray);


            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            // Сооздаем материал и источник света

       
            foreach (var planet in planets)
            {
                DrawObject(planet.Value);
            }
            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Light0);
            GL.Disable(EnableCap.Light1);
            GL.Disable(EnableCap.Lighting);
            window.SwapBuffers();


        }

        private void CreatePlanets()
        { 
           
            var mercuryPos = (float)Constant.SunR + (float)Constant.MercuryR + 1200;
            var venusPos = mercuryPos + (float) Constant.VenusR + (float)Constant.MercuryR + 500;
            var earthPos = venusPos + (float) Constant.VenusR + (float) Constant.EarthR + 500;
            var marsPos = earthPos + (float)Constant.EarthR + (float)Constant.MarsR + 500;

            var NeptPos = earthPos + (float)Constant.EarthR + (float)Constant.MarsR + 500;
            planets = new Dictionary<string, IPlanet>(9)
            {
                {Constant.Sun, new Planet(Constant.SunR, new Vector3(0, 0, 0),0.07)},
                {Constant.Mars, new Planet(Constant.MarsR,  new Vector3(-marsPos, marsPos / 1.5f,0),0.45/300)},
                {Constant.Venus, new Planet(Constant.VenusR, new Vector3(-venusPos, venusPos / 1.5f,0), 0.75/300)},
                {Constant.Mercury, new Planet(Constant.MercuryR,new Vector3(-mercuryPos , mercuryPos / 1.5f, 0),0.9/300)},
                //{Constant.Neptune, new Planet(Constant.NeptuneR,  new Vector3(-NeptPos, NeptPos/ 1.5f, 0))},
                //{Constant.Saturn, new Planet(Constant.SaturnR,  new Vector3((-1)*((float)(Constant.SunR + Constant.SaturnR) + 1427/50), 0, 0))},
                {Constant.Earth, new Planet(Constant.EarthR,  new Vector3(-earthPos, earthPos / 1.5f, 0), 0.9/1580)},
                //{Constant.Uranus, new Planet(Constant.UranusR,  new Vector3((-1)*((float)(Constant.SunR + Constant.UranusR) + 2869/50), 0, 0))},
                //{Constant.Jupiter, new Planet(Constant.JupiterR,  new Vector3((-1)*((float)(Constant.SunR + Constant.JupiterR) + 778/50), 0, 0))}
            };
        }

        private void Loaded(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, window.Width, window.Height);
            GL.MatrixMode(MatrixMode.Projection);            
            GL.Ortho(-50.0, 50.0, -50.0, 50.0, -1.0, 1.0);
            //var matrix4 = Matrix4.CreatePerspectiveFieldOfView(45.0f, (float)window.Width / window.Height, 1.0f, 100.0f);
            //GL.LoadMatrix(ref matrix4);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            
            CreatePlanets();
      
            GL.ClearColor(Color.Black);

            foreach (var keyValuePair in planets)
            {
                var planet = (Planet)keyValuePair.Value;
                planet.Name = keyValuePair.Key;
                var planetName = keyValuePair.Key;
                switch (planetName)
                {
                    case "earth":
                        planet.CreateBitMap(new Bitmap(Constant.EarthTexturePath));
                        break;
                    case "jupiter":
                        planet.CreateBitMap(new Bitmap(Constant.JupiterTexturePath));
                        break;
                    case "sun":
                        planet.CreateBitMap(new Bitmap(Constant.SunTexturePath));
                        break;
                    case "mercury":
                        planet.CreateBitMap(new Bitmap(Constant.MercuryTexturePath));
                        break;
                    case "neptune":
                        planet.CreateBitMap(new Bitmap(Constant.NeptuneTexturePath));
                        break;
                    case "venus":
                        planet.CreateBitMap(new Bitmap(Constant.VenusTexturePath));
                        break;
                    case "uranus":
                        planet.CreateBitMap(new Bitmap(Constant.UranusTexturePath));
                        break;
                    case "saturn":
                        planet.CreateBitMap(new Bitmap(Constant.SaturnTexturePath));
                        break;
                    case "mars":
                        planet.CreateBitMap(new Bitmap(Constant.MarsTexturePath));
                        break;

                    default:
                        break;
                }

            }

            // double crds = 10 * ((Planet)planets[Constant.Sun]).R;
            //double crds = ((Planet)planets[Constant.Mars]).R * 140;
            double crds = 10 * 250;
            GL.Viewport(0, 0, window.Width, window.Height);
            // Формируем матрицу проецирования
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-crds, crds, -crds, crds, -crds , crds);
            GL.Translate(-1000, 0, 0);
            // Умножаем матрицу проецирования на матрицы поворотов вокруг осей Y и X
            //GL.Rotate(15, new Vector3d(0, 1, 0));
            GL.Rotate(-70, new Vector3d(1, 0, 0));
        }

        private void DrawObject(IPlanet planet)
        {
            var p = (Planet) planet;

            GL.PushMatrix();
            
            if (p.Bitmap != null)
            {
                p.CreateTexture();
            }
            p.Move(z: 1.0);

            //GL.Translate(0, (float) Math.Sin(p.Angle) * 42.0f, 220.0f * (float) Math.Cos(p.Angle));
            if (!p.Name.Equals(Constant.Sun))
            {
                var c = -((Math.Pow(p.Pos.X,2)-Math.Pow(p.Pos.Y,2))/(2*p.Pos.X));
                var a = p.Pos.X + c;
                GL.Translate(a * Math.Cos(p.Angle) + c , p.Pos.Y * Math.Sin(p.Angle),0);
                p.MoveAround(2, z: 1.0);
            }
            GL.Begin(p.Type);
            p.CreateSphere();
            GL.End();
            GL.PopMatrix();

        }

        // Модель освещенности с одним источником цвета
        // Создает материал и источник света
        public void makeMatAndLight()
        {
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.Normalize);
            //GL.Normal3(0,0,0);
            float[] light_position = {-15, 0 ,0,0};
            float[] light1_position = { 0, 0 , 0 };
            float[] light_diffuse = { 255.0f, 255.0f, 220.0f };
            GL.Light(LightName.Light0, LightParameter.Position, light_position);
            GL.Light(LightName.Light0, LightParameter.Diffuse, light_diffuse);
            //GL.Light(LightName.Light1, LightParameter.SpotDirection, light1_position);
            GL.Light(LightName.Light1, LightParameter.Diffuse, light_diffuse);
            GL.Enable(EnableCap.Light0);
            //GL.Enable(EnableCap.Light1);


        }
        // Создает 2D-текстуру на базе растрового образа, загруженного в data1
    }
}
