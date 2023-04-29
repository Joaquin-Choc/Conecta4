using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Proyecto_no._2_Conecta_4
{
    internal class Tiempo
    {
        public static Stopwatch tiempo = new Stopwatch();
        public static void tiempoInicial()
        {
            tiempo = new Stopwatch();
            tiempo.Start();
        }
        public static string tiempoFinal()
        {
            string tiempoTotal = "";
            tiempo.Stop();
            TimeSpan TT = tiempo.Elapsed;
            tiempoTotal = TT.ToString("mm':'ss");
            return tiempoTotal;
        }
    }
}
