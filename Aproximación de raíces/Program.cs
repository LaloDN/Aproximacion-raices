using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Aproximación_de_raíces
{
    class Program
    {
        static void Main(string[] args)
        {
            short s,m;
            string aux;
            float x1, x2, p, pm,pos,f1,f2;
            bool ex;
            Ecuacion ec = new Ecuacion();
            Console.WriteLine("=====Bienvenido====");
            do
            {
                // ec.Coeficientes();
                //Segmento para elegir los primeros valores de xl y xu.
                do
                {
                    Console.WriteLine("Introduzca un valor numerico para xl:");
                    aux = Console.ReadLine();
                    x1 = float.Parse(aux);
                    Console.WriteLine("Introduzca un valor numerico para xu:");
                    aux = Console.ReadLine();
                    x2 = float.Parse(aux);
                    ex = ec.Bolsano(x1, x2);
                } while (ex == false);
                //Ajuste de error deseado.
                do
                {
                    Console.WriteLine("Ahora introduzca el porcentaje de error que desea tener a lo mucho:");
                    aux = Console.ReadLine();
                    p = float.Parse(aux);
                    if (p < 0)
                    {
                        Console.WriteLine("Error: Introduzca un porcentaje valido (al menos mayor o igual del 0%).");
                    }
                } while (p < 0);
                //Selección de método.
                Console.WriteLine("Ecuación: (e^-x)-x");
                Console.WriteLine("Métodos disponibles:");
                Console.WriteLine("\n\t1-Método de la bisección\n\t2-Método de la falsa posición");
                Console.WriteLine("\n¿Por que método desea encontrar las raíces de la ecuación?");
                do
                {
                    Console.WriteLine("Introduzca su respuesta:");
                    m = Convert.ToSByte(Console.ReadLine());
                    if(m!=1 && m != 2)
                    {
                        Console.WriteLine("Error: Introduzca que método quiere utilizar (1 o 2).");
                    }
                } while (m != 1 && m != 2);
                //Algo de estetica...
                Console.WriteLine("Buscando el resultado más adecuado respecto a los parámetros, espere por favor");
                Thread.Sleep(800);
                Console.WriteLine(".");
                Thread.Sleep(800);
                Console.WriteLine(".");
                Thread.Sleep(800);
                Console.WriteLine(".");
                Thread.Sleep(800);
                if (m == 1)
                {
                    //Si el usuario eligió el método de la bisección, se ejecutará este bloque de instrucciones.
                    pm = ec.PuntoMedio(x1, x2);
                    ec.Biseccion(x1, x2, pm, p, 0);
                }
                else
                {
                    //Aquí se ejecutará el método de la posición falsa, se tiene que hacer el proceso para una primera itearción.
                    f1 = ec.Evaluar(x1);
                    f2 = ec.Evaluar(x2);
                    pos = ec.ObtenerPosición(x1,x2,f1,f2);
                    ec.PosicionFalsa(x1, x2, pos, p, 0);
                }
                Console.WriteLine("\n¿Desea hacer alguna otra prueba?");
                Console.WriteLine("1-Si\t2-No (Salir de la aplicación)");
                do
                {
                    Console.WriteLine("Su respuesta:");
                    s = Convert.ToSByte(Console.ReadLine());
                    if (s != 1 && s != 2)
                        Console.WriteLine("Por favor, introduzca una respuesta valida.");
                } while (s != 1 && s != 2);
                Console.Clear();
            } while (s != 2);
            Console.WriteLine("Presione cualquier tecla para salir de la aplicación.");
            Console.ReadKey();
        }
    }
}
