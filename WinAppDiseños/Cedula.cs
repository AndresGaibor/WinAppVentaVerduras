using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppDiseños
{
    internal class Cedula
    {
        string cedula; 
        
        public Cedula(string c)
        {
            cedula = c;
        }

        public bool ComprobarCedula()
        {
            int sumI = 0 ;
            int sumP = 0;
            int sumT = 0;

            for (int i = 0; i < 9; i++)
            {
                int num = int.Parse(cedula[i].ToString());

                if ((i + 1) % 2 != 0)
                {
                    if ((num * 2) > 9) sumI += num * 2 - 9;
                    else sumI += num * 2;
                } 

                else sumP += num;
            }

            sumT = sumI+sumP;

            int res = sumT % 10;

            if (res != 0) res = 10 - res;

            if (res == int.Parse(cedula[9].ToString())) return true;
            else return false;

        }
    }
}
