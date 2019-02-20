using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open
{
    class Constant
    {
        private static readonly string Path = "texture\\";
        public static readonly string EarthTexturePath = Path + "earth.jpg";
        public static readonly string JupiterTexturePath = Path + "jupiter.jpg";
        public static readonly string MarsTexturePath = Path + "mars.jpg";
        public static readonly string VenusTexturePath = Path + "venus.jpg";
        public static readonly string MercuryTexturePath = Path + "mercury.jpg";
        public static readonly string UranusTexturePath = Path + "uranus.jpg";
        public static readonly string NeptuneTexturePath = Path + "neptune.jpg";
        public static readonly string SaturnTexturePath = Path + "saturn.jpg";
        public static readonly string SunTexturePath = Path + "sun.jpg";
        public static readonly string Sun = "sun";
        public static readonly string Earth = "earth";
        public static readonly string Jupiter = "jupiter";
        public static readonly string Mars = "mars";
        public static readonly string Venus = "venus";
        public static readonly string Saturn = "saturn";
        public static readonly string Uranus = "uranus";
        public static readonly string Mercury = "mercury";
        public static readonly string Neptune = "neptune";
        public static readonly double SunR = 100;
        public static readonly double EarthR = 50;
        public static readonly double MercuryR = EarthR / 0.8;
        public static readonly double VenusR = EarthR / 0.9;
        public static readonly double MarsR = EarthR / 2.5;
        public static readonly double JupiterR = EarthR / 9.4;
        public static readonly double SaturnR = EarthR / 4;
        public static readonly double UranusR = EarthR / 3.9;
        public static readonly double NeptuneR = EarthR / 0.5;
    }
}
