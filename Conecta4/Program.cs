using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_no._2_Conecta_4
{
    class Conecta4
    {
        //información general que se utilizara para los jugadores
        static int Filas = 6;
        static int Columnas = 7;
        static string[,] tablero1 = new string[6, 7];
        static int Conectar = 4;
        static string Jugador1 = "X";
        static string Jugador2 = "O";
        static string Vacio = " ";
        //Menú
        static int JugadorVsJugador = 1;
        static int JugadorVsCompu = 2;
        static int TablaPuntuación = 3;
        static int CerrarJuego = 4;
        //extra para validar las filas y columnas
        static int ErrorFila = 100;
        static int ErrorColumna = 101;
        static int ErrorN = 102;
        static int ConectA = 103;
        static int ConectBD = 200;
        static int ConectAD = 2001;
        static int NConect = 202;
        static int FNEncontrada = 203;
        static int ConectD = 204;
        static string JugadorC = Jugador2;

        static string[,] clonarMatriz(string[,] tableroOriginal)
        {
            return tableroOriginal.Clone() as string[,];
        }


        static int elegirColumnaC(string jugador, string[,] tablero)
        {
            int columnas = tablero.GetLength(1);
            Random rnd = new Random();
            int ColumnaAlt = rnd.Next(columnas);

            return ColumnaAlt;
        }
        // pedir al jugador colocar la pieza
        static int solicitarColumnaJugador()
        {
            Console.Write("Elije una columna para colocar su ficha: ");

            int columna = Convert.ToInt32(Console.ReadLine());
            columna--;
            return columna;
        }

        static string elegirJugadorAlAzar()
        {
            int numero = aleatorioRango(0, 1);
            if (numero == 1)
            {
                return Jugador1;
            }
            else
            {
                return Jugador2;
            }
        }

        static string obtenerOponente(string jugador)
        {
            if (jugador == Jugador1)
            {
                return Jugador2;
            }
            else
            {
                return Jugador1;
            }
        }

        static int aleatorioRango(int min, int max)
        {
            Random rnd = new System.Random();
            return rnd.Next(min, max + 1);
        }

        static int obtenerFilaD(int columna, string[,] tablero)
        {
            int i;
            for (i = Filas - 1; i >= 0; i--)
            {
                if (tablero[i, columna] == Vacio)
                {
                    return i;
                }
            }
            return FNEncontrada;
        }

        static int cFicha(string jugador, int columna, string[,] tablero)
        {

            if (columna < 0 || columna >= Columnas)
            {
                return ErrorFila;
            }
            int fila = obtenerFilaD(columna, tablero);
            if (fila == FNEncontrada)
            {
                return ErrorColumna;
            }
            tablero[fila, columna] = jugador;
            return ErrorN;

        }

        static void limpiarTablero(string[,] tablero)
        {
            int i;
            for (i = 0; i < Filas; ++i)
            {
                int p;
                for (p = 0; p < Columnas; ++p)
                {
                    tablero[i, p] = Vacio;
                }
            }
        }
        static void dibujarEncabezado(int columnas)
        {
            Console.Write("\n");
            int i;
            for (i = 0; i < columnas; ++i)
            {
                Console.Write("[" + (i + 1) + "]");

            }
        }

        static int dibujarTablero(string[,] tablero)
        {
            dibujarEncabezado(Columnas);
            Console.Write("\n");
            int i;
            for (i = 0; i < Filas; ++i)
            {
                int j;
                for (j = 0; j < Columnas; ++j)
                {
                    Console.Write("[" + tablero[i, j] + "]");
                }
                Console.Write("\n");
            }
            return 0;
        }
        static bool esEmpate(string[,] tablero)
        {
            int i;
            for (i = 0; i < Columnas; ++i)
            {
                int resultado = obtenerFilaD(i, tablero);
                if (resultado != FNEncontrada)
                {
                    return false;
                }
            }
            return true;
        }
        static int contarArriba(int x, int y, string jugador, string[,] tablero)
        {
            int YI = (y - Conectar >= 0) ? y - Conectar + 1 : 0;
            int contador = 0;
            for (; YI <= y; YI++)
            {
                if (tablero[YI, x] == jugador)
                {
                    contador++;
                }
                else
                {
                    contador = 0;
                }
            }
            return contador;
        }

        static int contarD(int x, int y, string jugador, string[,] tablero)
        {
            int XF = (x + Conectar < Columnas) ? x + Conectar - 1 : Columnas - 1;
            int contador = 0;
            for (; x <= XF; x++)
            {
                if (tablero[y, x] == jugador)
                {
                    contador++;
                }
                else
                {
                    contador = 0;
                }
            }
            return contador;
        }

        static int contarAD(int x, int y, string jugador, string[,] tablero)
        {
            int XF = (x + Conectar < Columnas) ? x + Conectar - 1 : Columnas - 1;
            int YI = (y - Conectar >= 0) ? y - Conectar + 1 : 0;
            int contador = 0;
            while (x <= XF && y <= YI)
            {
                if (tablero[y, x] == jugador)
                {
                    contador++;
                }
                else
                {
                    contador = 0;
                }
                x++;
                y++;
            }
            return contador;
        }

        static int contarBD(int x, int y, string jugador, string[,] tablero)
        {
            int XF = (x + Conectar < Columnas) ? x + Conectar - 1 : Columnas - 1;
            int YF = (y + Conectar < Filas) ? y + Conectar - 1 : Filas - 1;
            int contador = 0;
            while (x <= XF && y <= YF)
            {
                if (tablero[y, x] == jugador)
                {
                    contador++;
                }
                else
                {
                    contador = 0;
                }
                x++;
                y++;
            }
            return contador;
        }

        



        // Para jugar
        static void jugar(int modo) ///CAMBIAR A INT
        {
            string[,] tablero = new string[Filas, Columnas];
            limpiarTablero(tablero);
            string nombre1;
            string nombre2;
            string jugadorAc = elegirJugadorAlAzar();
            Tiempo.tiempoInicial();
            Console.WriteLine("Ingrese el nombre del jugador 1 / Tu ficha sera X");
            nombre1 = Console.ReadLine();
            nombre1 = Jugador1;
            Console.WriteLine("Ingrese el nombre del jugador 2 / tu ficha sera O");
            nombre2 = Console.ReadLine();

            while (true)
            {
                int columna = 0;
                Console.WriteLine("\nTurno del jugador " + jugadorAc);
                dibujarTablero(tablero);
                if (modo == JugadorVsCompu)
                {
                    nombre2 = "Computadora";
                    if (jugadorAc == JugadorC)
                    {
                        columna = elegirColumnaC(jugadorAc, tablero);
                    }
                    else
                    {
                        columna = solicitarColumnaJugador();
                    }
                }

                else if (modo == JugadorVsJugador)
                {

                    columna = solicitarColumnaJugador();

                }
                int estado = cFicha(jugadorAc, columna, tablero);
                if (estado == ErrorColumna)
                {
                    Console.Write("Error: La columna está llema, elegir nueva columna");
                }
                else if (estado == ErrorFila)
                {
                    Console.Write("Error, la fila no es correcta");
                }
                else if (estado == ErrorN)
                {
                    int g = ganador(jugadorAc, tablero);
                    if (g != NConect)
                    {
                        dibujarTablero(tablero);
                        Console.WriteLine("Ganador es el jugador " + jugadorAc);
                        Console.WriteLine(Tiempo.tiempoFinal());
                        Console.ReadKey();
                        Console.WriteLine("\n Fin del juego");
                        Console.WriteLine("Presione cualquier tecla para volver al menu principal");
                        Console.ReadKey();
                        break;
                        //RETORNE 1 SI ES P1 o 2 SI ES P2
                        //PARAMETRO = jugadorAC
                    }
                    else if (esEmpate(tablero))
                    {
                        dibujarTablero(tablero);
                        Console.Write("Empate");
                        Console.WriteLine(Tiempo.tiempoFinal());
                        Console.WriteLine("\n Fin del juego");
                        Console.WriteLine("Presione cualquier tecla para volver al menu principal");
                        Console.ReadKey();
                        break;
                    }
                }
                else if (modo == TablaPuntuación)
                {
                }

                jugadorAc = obtenerOponente(jugadorAc);

            }

        }
        static int ganador(string jugador, string[,] tablero)
        {
            int y;
            for (y = 0; y < Filas; y++)
            {
                int x;
                for (x = 0; x < Columnas; x++)
                {
                    int ConteoA = contarArriba(x, y, jugador, tablero);
                    if (ConteoA >= Conectar)
                    {
                        return ConectA;
                    }
                    if (contarD(x, y, jugador, tablero) >= Conectar)
                    {
                        return ConectD;
                    }
                    if (contarAD(x, y, jugador, tablero) >= Conectar)
                    {
                        return ConectAD;
                    }
                    if (contarBD(x, y, jugador, tablero) >= Conectar)
                    {
                        return ConectBD;
                    }
                }
            }
            return NConect;
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido a Conect 4");
            Console.WriteLine($"{JugadorVsJugador} Jugador contra Jugador");
            Console.WriteLine($"{JugadorVsCompu} Jugador contra Computadora");
            Console.WriteLine($"{TablaPuntuación} Tabla de puntuación");
            Console.WriteLine($"{CerrarJuego} Salir del Juego");
            Console.Write("Elegir modo de juego ");
            int modo = Convert.ToInt32(Console.ReadLine());
            if (modo != JugadorVsJugador && modo != JugadorVsCompu && modo != TablaPuntuación)
            {
                Console.WriteLine("Saliendo del juego");
                return;
            }
            jugar(modo);
        }
    }
}

