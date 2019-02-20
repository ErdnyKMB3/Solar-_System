using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open
{
    interface IPlanet
    {
        void CreateSphere(int nx = 32, int ny = 32);
        void CreateTexture();
        void Move(double x = 0, double y = 0, double z = 0);
    }
}
