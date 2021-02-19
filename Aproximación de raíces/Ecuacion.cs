/* CAMBIOS EN ESTA VERSIÓN 2.0:
   - El nombre de la aplicación se estableció como "Aproximación de raíces".
   - El método Coeficientes fue removido por resultar redudante en esta versión.
   - Los atributos de los coeficientes x1,x2 y x3 han sido removidos de la clase.
   - El método de evaluar fue cambiado, en lugar de evaluar una función de segundo orden ahora se evalua una función exponencial.
   - El método Extremos fue renombrado por "Bolsano".
   - Se ha agregado un nuevo método para calcular el error porcental actual y se ha incorporado el viejo código
   - Se ha agregado un nuevo método: PosicionFalsa, simula el proceso de sacar aproximación de raíces con el mismo nombre.

   Fecha de esta versión 19 de febrero del 2021
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aproximación_de_raíces
{
    class Ecuacion
    {

        public Ecuacion() { }

        /*
        public void Coeficientes()
        {
            short ok;
            string c1, c2, c3;
            float disc;
            do
            {
                Console.WriteLine("Escriba el coeficiente del termino cuadrático:");
                c1 = Console.ReadLine();
                Console.WriteLine("Escriba el coeficiente del termino lineal:");
                c2 = Console.ReadLine();
                Console.WriteLine("Escriba el coeficiente independiente:");
                c3 = Console.ReadLine();
                Console.WriteLine("Su ecuación es: {0}x²+({1})x+({2})\n¿Es correcto?", c1, c2, c3);
                do
                {
                    Console.WriteLine("Su respuesta (1-Si 2-No):");
                    ok = Convert.ToSByte(Console.ReadLine());
                } while (ok != 1 && ok != 2);
                disc = (float)Math.Pow(float.Parse(c2), 2) - 4 * float.Parse(c1) * float.Parse(c3);
                if (disc < 0)
                {
                    Console.WriteLine("Advertencia: Al parecer ha introducido una combinación de coeficientes que generan una ec. con raices imaginarias, pruebe con otros valores.");
                }
            } while (ok != 1 || disc < 0);
            this.x1 = float.Parse(c1);
            this.x2 = float.Parse(c2);
            this.x3 = float.Parse(c3);
        }*/

        //NOTA
        //Este método de evaluar sufrió cambios, en vez de calcular 
        public float Evaluar(float x)
        {
            float e = 2.71828f;
            float resultado = (float)Math.Pow(e,-x)-x;
            return resultado;
        }

        //NOTA
        //Esta función tenia el nombre de "Extremos", se renombró como "Bolsano"
        public bool Bolsano(float izq, float der)
        {
            float fxi, fxd;
            fxi = Evaluar(izq);
            fxd = Evaluar(der);
            if (fxi * fxd < 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Los valores que ha introducido para xl y xu no cumplen con la condición de f(xl)f(xi)<0");
                Console.WriteLine("\tResultado de f(xl):" + fxi);
                Console.WriteLine("\tResultado de f(xu):" + fxd);
                Console.WriteLine("\tResultado del producto:" + fxi * fxd);
                Console.WriteLine("Por favor, intente con otros valores.");
                return false;
            }
        }

        public float PuntoMedio(float n1, float n2)
        {
            float pm = (n1 + n2) / 2;
            return pm;
        }

        public float ObtenerPosición(float xl,float xu,float fxl, float fxu)
        {
            float xr = xu - ((fxu * (xl - xu)) / (fxl - fxu));
            return xr;
        }

        //NUEVO MÉTODO
        //Método para devolver el error porcentual actual
        public float ErrorPorcentual(float AA, float AP)
        {
            float EPA = ((AA - AP) / AA) * 100;
            //Esto vendría a ser el valor absoluto que hay en la formula.
            if (EPA < 0)
            {
                EPA *= (-1);
            }
            return EPA;
        }

        public void Biseccion(float xl, float xu, float xr, float P, int i)
        {
            float fxl, fxu, fxr, EA, neoxr;
            fxl = Evaluar(xl);
            fxr = Evaluar(xr);
            fxu = Evaluar(xu);
            Console.WriteLine("\n**ITERACIÓN {0}**", i + 1);
            Console.WriteLine("Valor de xl:" + xl);
            Console.WriteLine("Valor de xu:" + xu);
            Console.WriteLine("Valor de xr:" + xr);
            //Paso 3 de la metodología: Actualizar un extremo.
            if ((fxl * fxr) < 0)
            {
                xu = xr;
            }
            else
            {
                xl = xr;
            }
            //Paso 4 de la metdología: Sacar una nueva aproximación
            neoxr = PuntoMedio(xl, xu);
            Console.WriteLine("Nuevo valor aproximado:" + neoxr);
            //Paso 5 de la metdología: Calcular el error aproximado y ver si cumple con el error
            EA = ErrorPorcentual(neoxr, xr);
            Console.WriteLine("Porcentaje de error actual:{0}%", EA);
            if (EA <= P)
            {
                Console.WriteLine("\n\n\nSe ha encontrado un valor de acuerdo su búsqueda!!!!!");
                Console.WriteLine("\n\t======REPORTE======");
                Console.WriteLine("\nPorcentaje deseado: A lo mucho {0}%\nValor Encontrado:{1}\nMargen de error:{2}%,\nNúmero de iteraciones:{3}", P, neoxr, EA, i + 1);
                Console.WriteLine("Valor final de xl:{0}\nValor final de xu:{1}", xl, xu);
                Console.WriteLine("\n\t===================\n");
            }
            else
            {
                if (i <= 100)
                {
                    Biseccion(xl, xu, neoxr, P, i + 1);
                }
                else
                {
                    Console.WriteLine("\nSe han alcanzado hasta 100 iteraciones, desplegando el último resultado encontrado...");
                    Console.WriteLine("\n\t======REPORTE======");
                    Console.WriteLine("\nPorcentaje deseado: A lo mucho {0}%\nValor Encontrado:{1}\nMargen de error:{2}%,\nNúmero de iteraciones:{3}", P, neoxr, EA, i + 1);
                    Console.WriteLine("Valor final de xl:{0}\nValor final de xu:{1}", xl, xu);
                }
            }
        }

        //NUEVO MÉTODO
        //Este es el método de aproximar las raíces, pero con el método del posicionamiento falso
        public void PosicionFalsa(float xl, float xu, float xr, float P, int i)
        {
            float fxl, fxu, fxr, EA, neoxr;
            fxl = Evaluar(xl);
            fxr = Evaluar(xr);
            fxu = Evaluar(xu);
            Console.WriteLine("\n**ITERACIÓN {0}**", i + 1);
            Console.WriteLine("Valor de xl:" + xl);
            Console.WriteLine("Valor de xu:" + xu);
            Console.WriteLine("Valor de xr:" + xr);
            //Paso 3 de la metodología: Actualizar un extremo.
            if ((fxl * fxr) < 0)
            {
                xu = xr;
            }
            else
            {
                xl = xr;
            }
            //Paso 4: Obtener un nuevo punto con el método de la falsa posición
            neoxr = ObtenerPosición(xl, xu, fxl, fxu);
            Console.WriteLine("Nuevo valor aproximado:" + neoxr);
            //Paso 5: Calcular el procentaje de error actual
            EA = ErrorPorcentual(neoxr, xr);
            Console.WriteLine("Porcentaje de error actual:{0}%", EA);
            //Ahora preguntamos si el error de porcentaje actual es menor al del deseado
            if (EA <= P)
            {
                Console.WriteLine("\n\n\nSe ha encontrado un valor de acuerdo su búsqueda!!!!!");
                Console.WriteLine("\n\t======REPORTE======");
                Console.WriteLine("\nPorcentaje deseado: A lo mucho {0}%\nValor Encontrado:{1}\nMargen de error:{2}%,\nNúmero de iteraciones:{3}", P, neoxr, EA, i + 1);
                Console.WriteLine("Valor final de xl:{0}\nValor final de xu:{1}", xl, xu);
                Console.WriteLine("\n\t===================\n");
            }
            else
            {
                if (i <= 100)
                {
                    PosicionFalsa(xl, xu, neoxr, P, i + 1);
                }
                else
                {
                    Console.WriteLine("\nSe han alcanzado hasta 100 iteraciones, desplegando el último resultado encontrado...");
                    Console.WriteLine("\n\t======REPORTE======");
                    Console.WriteLine("\nPorcentaje deseado: A lo mucho {0}%\nValor Encontrado:{1}\nMargen de error:{2}%,\nNúmero de iteraciones:{3}", P, neoxr, EA, i + 1);
                    Console.WriteLine("Valor final de xl:{0}\nValor final de xu:{1}", xl, xu);
                }
            }

        }
    }
}
