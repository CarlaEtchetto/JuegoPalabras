using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtchettoCarla
{
    class Program

    {
        public static void ImpresionMenu()
        {
            Console.WriteLine("******************************************************");
            Console.WriteLine(" * 1.Jugar                                           *");
            Console.WriteLine(" * 2.Instrucciones                                   *");
            Console.WriteLine(" * 3.Cambiar cantidad de palabras                    *");
            Console.WriteLine(" * 4.Seleccionar modo Palabra oculta                 *");
            Console.WriteLine(" * 5.Seleccionar modo Palabra desordenada            *");
            Console.WriteLine(" * 6.Ver reporte de la última partida                *");
            Console.WriteLine(" * 7.Salir                                           *");
            Console.WriteLine("******************************************************");
        }
        public static bool MododeJugar(int opcion, ref bool palabraOculta)
        {
            if (opcion == 4)
                palabraOculta = true;
            else if (opcion == 5)
                palabraOculta = false;

            return palabraOculta;
        }

        public static void ImpresionModo(bool palabraOculta, int cantPalab)
        {
            if (palabraOculta == true)
                Console.WriteLine("Modo Palabra oculta. " + "Cantidad palabras: " + cantPalab);

            else
                Console.WriteLine("Modo Palabra desordenada. " + "Cantidad de palabras: " + cantPalab);

        }
        public static int ChequearOpcion(int opcion)
        {
            bool opcionValida = false;

            string opcionTxt = Console.ReadLine();
            Console.Clear();
            opcionValida = int.TryParse(opcionTxt, out opcion);
            if (opcionValida == false)
            {
                ImprimirError("Ingrese una opción correcta");
                Console.ReadLine();
            }
            if (opcion > 7)
            {
                Console.Clear();
                ImprimirError("Ingrese una opción correcta");
                Console.ReadLine();
                Console.Clear();
            }
            else
                return opcion;

            return opcion;
        }
        public static void ImprimirError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }
        public static void ImprimirMensaje(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }

        public static void Jugar(string[] gamer, string[] gamer2, bool palabraOculta, string[] rtaGamer1, string[] rtaGamer2, ref int jugador1, ref int jugador2, ref List<string> Report, ref List<string> Pistas, ref List<string> Puntaje)
        {

            jugador1 = 0;
            jugador2 = 0;
            Console.WriteLine("Jugador 1 es su turno de ingresar las palabras");
            gamer = IngresodePalabra(gamer);
            Console.WriteLine("Jugador 2 es su turno de ingresar las palabras");
            gamer2 = IngresodePalabra(gamer2);

            for (int i = 0; i < gamer2.Length; i++)
            {
                Console.WriteLine($"Jugador {1} adivine la palabra {i + 1}:  ");
                if (palabraOculta == true)
                    ImpresiondePistaOculta(gamer2, i, Pistas);
                else
                    ImpresionPistaDesordenada(gamer2, i, Pistas);
                Console.WriteLine();
                Console.WriteLine("Jugador 1 ingrese respuesta: ");
                rtaGamer1[i] = Console.ReadLine().ToLower();
                if (rtaGamer1[i] == gamer2[i])
                {
                    jugador1++;
                    Report.Add("El jugador 1 ha acertado la palabra " + gamer2[i] + " con la pista");
                    Puntaje.Add(" - Puntaje 1 puntos.");
                }
                else
                {
                    Report.Add("El jugador 1 ha errado la palabra " + gamer2[i] + " con la pista");
                    Puntaje.Add(" - Puntaje 0 puntos.");
                }
                Console.Clear();

                Console.WriteLine($"Jugador {2} adivine la palabra {i + 1}:  ");
                if (palabraOculta == true)
                    ImpresiondePistaOculta(gamer, i, Pistas);
                else
                    ImpresionPistaDesordenada(gamer, i, Pistas);
                Console.WriteLine();
                Console.WriteLine("Jugador 2 ingrese respuesta: ");
                rtaGamer2[i] = Console.ReadLine().ToLower();
                if (rtaGamer2[i] == gamer[i])
                {
                    jugador2++;
                    Report.Add("El jugador 2 ha acertado la palabra " + gamer[i] + " con la pista");
                    Puntaje.Add(" - Puntaje 1 puntos.");
                }
                else
                {
                    Report.Add("El jugador 2 ha errado la palabra " + gamer[i] + " con la pista");
                    Puntaje.Add(" - Puntaje 0 puntos.");
                }
                Console.Clear();

            }

            Console.Clear();
            ImpresionResultado(jugador1, jugador2);
            Console.WriteLine("Presione Enter para continuar");
            Console.ReadLine();
            Console.Clear();

        }
        public static string[] IngresodePalabra(string[] gamer)
        {

            for (int i = 0; i < gamer.Length; i++)
            {
                do
                {
                    Console.Write($"Indique palabra {i + 1}: ");
                    gamer[i] = Console.ReadLine().ToLower();
                    Console.Clear();
                    if (gamer[i].Length < 5)
                    {
                        ImprimirError("Ingrese una palabra con 5 o más caracteres");
                        Console.ReadLine();
                        Console.Clear();
                    }
                } while (gamer[i].Length < 5);
            }

            return gamer;
        }
        public static void ImpresionPistaDesordenada(string[] gamer, int i, List<string> Pistas)
        {
            int x = 0;
            char[] pista;

            pista = gamer[i].ToCharArray();
            char[] pistaAux = new char[pista.Length];

            do
            {
                if (x % 2 != 0)
                {
                    Console.Write(pista[x - 1]);
                    pistaAux[x] = pista[x - 1];
                }
                else if (x % 2 == 0 && x < pista.Length - 1)
                {
                    Console.Write(pista[x + 1]);
                    pistaAux[x] = pista[x + 1];

                }

                else
                {
                    Console.Write(pista[x]);
                    pistaAux[x] = pista[x];
                }

                x++;
            } while (x < pista.Length);

            string pistaString = new string(pistaAux);
            Pistas.Add(pistaString);
        }

        public static void ImpresiondePistaOculta(string[] gamer, int i, List<string> Pistas)
        {
            int x = 0;
            char[] pista = new char[char.MaxValue];
            pista = gamer[i].ToCharArray();
            do
            {
                if (x == 0)
                    Console.Write(pista[x]);
                else if (x == pista.Length / 2)
                    Console.Write(pista[x]);
                else if (x == pista.Length - 1)
                    Console.Write(pista[x]);
                else
                {
                    Console.Write("_");
                    pista[x] = '_';
                }
                x++;
            } while (x < pista.Length);

            string pistaString = new string(pista);
            Pistas.Add(pistaString);

        }
        public static void ImpresionInstruciones()
        {
            Console.Clear();
            Console.WriteLine("INSTRUCCIONES\n" +
            "El juego comienza con cada jugador ingresando su lista de palabras. Luego, elije al azar el turno del primer jugador.\n" +
            "Cada jugador debe en su turno intentar adivinar la palabra, si acierta suma un punto, si no suma un error\n" +
            "El juego finaliza cuando no quedan palabras por adivinar, siendo el ganador, quien adivino más palabras\n ");
            ImprimirMensaje("Presione Enter para voler al Menú Principal");
            Console.ReadLine();
            Console.Clear();

        }
        public static int ChequeoCantidadPalabras(int cantPalabras)
        {
            int cantAux;
            bool cantValida = false;
            Console.Clear();
            Console.Write("Ingrese cantidad de palabras: ");
            string cantTxt = Console.ReadLine();
            Console.Clear();
            cantValida = int.TryParse(cantTxt, out cantAux);

            if (cantValida == false || cantAux < 3)
            {
                ImprimirError("Se seguirá utilizando el último valor: " + cantPalabras);
                Console.ReadLine();
            }

            else
                cantPalabras = cantAux;
            return cantPalabras;
        }
        public static void Reporte(ref List<string> Report, ref List<string> Pistas, ref List<string> Puntaje)
        {
            for (int i = 0; i < Report.Count; i++)
            {
                Console.Write(Report[i] + " ");
                Console.Write(Pistas[i] + " ");
                Console.Write(Puntaje[i] + " ");
                Console.WriteLine();
            }

        }

        public static void ImpresionResultado(int jugador1, int jugador2)
        {
            if (jugador1 > jugador2)
                Console.WriteLine($"Ganador jugador 1 " + "con " + jugador1 + " palabras acertadas");
            else if (jugador2 > jugador1)
                Console.WriteLine($"Ganador jugador 2 " + "con " + jugador2 + " palabras acertadas");
            else if (jugador1 == jugador2)
                Console.WriteLine("Ha habido un empate, ambos jugadores acertaron " + jugador1 + " palabras");
        }

        static void Main(string[] args)
        {
            int opcion = 0, jugador1 = 0, jugador2 = 0, cantPalabras = 3;
            bool palabraOculta = true, partida = false;
            string[] gamer1 = new string[3];
            string[] gamer2 = new string[3];
            string[] rtaGamer1 = new string[3];
            string[] rtaGamer2 = new string[3];
            List<string> Report = new List<string>();
            List<string> Pistas = new List<string>();
            List<string> Puntaje = new List<string>();

            do
            {
                Console.Clear();
                ImpresionMenu();
                ImpresionModo(palabraOculta, cantPalabras);
                opcion = ChequearOpcion(opcion);
                palabraOculta = MododeJugar(opcion, ref palabraOculta);


                if (opcion == 1)

                {
                    Report.Clear();
                    Pistas.Clear();
                    Puntaje.Clear();
                    partida = true;
                    Console.Clear();
                    Jugar(gamer1, gamer2, palabraOculta, rtaGamer1, rtaGamer2, ref jugador1, ref jugador2, ref Report, ref Pistas, ref Puntaje);
                }

                else if (opcion == 2)
                {
                    ImpresionInstruciones();

                }

                else if (opcion == 3)

                {
                    cantPalabras = ChequeoCantidadPalabras(cantPalabras);
                    gamer1 = new string[cantPalabras];
                    gamer2 = new string[cantPalabras];
                    rtaGamer1 = new string[cantPalabras];
                    rtaGamer2 = new string[cantPalabras];
                    Console.Clear();

                }

                if (opcion == 6)
                {
                    if (partida == false)
                    {
                        Console.Clear();
                        ImprimirMensaje("No se encuentra disponible el relato de la última partida");
                        Console.ReadLine();
                    }

                    else
                    {
                        Console.Clear();
                        Reporte(ref Report, ref Pistas, ref Puntaje);
                        ImpresionResultado(jugador1, jugador2);
                        Console.ReadLine();
                        Console.Clear();
                    }
                }

            } while (opcion != 7);
            Console.Clear();
            Console.WriteLine("Adios!!");
            Console.ReadLine();

        }
    }
}