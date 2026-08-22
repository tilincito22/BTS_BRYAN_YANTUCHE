/*
 * BST_Bryan_Yantuche.cs
 * ---------------------------------------------------------------
 * Aplicacion de consola para administrar un Arbol Binario de
 * Busqueda (BST), implementada en C#.
 *
 * Funcionalidades:
 *  1. Insertar Nodo
 *  2. Buscar Nodo
 *  3. Mostrar Recorrido Inorden    (Izquierda - Raiz - Derecha)
 *  4. Mostrar Recorrido Preorden   (Raiz - Izquierda - Derecha)
 *  5. Mostrar Recorrido Postorden  (Izquierda - Derecha - Raiz)
 *  6. Salir
 *
 * La estructura de datos utilizada es exclusivamente un arbol
 * binario de busqueda enlazado mediante nodos (no se usan arreglos
 * ni listas para almacenar los valores del arbol).
 *
 * Insercion, busqueda y recorridos se implementan de forma
 * recursiva, tal como recomienda el enunciado.
 *
 * La consola se limpia automaticamente antes de mostrar el menu,
 * para mantener la interfaz ordenada.
 * ---------------------------------------------------------------
 */

using System;
using System.Text;

namespace BST_Bryan_Yantuche
{
    /// <summary>
    /// Representa un nodo del Arbol Binario de Busqueda.
    /// </summary>
    class Nodo
    {
        public int Valor;
        public Nodo Izquierdo;
        public Nodo Derecho;

        public Nodo(int valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }

    /// <summary>
    /// Clase que representa el Arbol Binario de Busqueda y sus operaciones.
    /// </summary>
    class ArbolBinarioBusqueda
    {
        private Nodo raiz;

        public ArbolBinarioBusqueda()
        {
            raiz = null;
        }

        // ---------------------- INSERTAR ----------------------

        /// <summary>
        /// Inserta un nuevo valor en el arbol si no existe previamente.
        /// </summary>
        /// <returns>true si se insertó, false si ya existía (duplicado)</returns>
        public bool Insertar(int valor)
        {
            if (Buscar(valor))
            {
                return false; // No se permiten duplicados
            }
            raiz = InsertarRecursivo(raiz, valor);
            return true;
        }

        private Nodo InsertarRecursivo(Nodo nodoActual, int valor)
        {
            if (nodoActual == null)
            {
                return new Nodo(valor);
            }

            if (valor < nodoActual.Valor)
            {
                nodoActual.Izquierdo = InsertarRecursivo(nodoActual.Izquierdo, valor);
            }
            else if (valor > nodoActual.Valor)
            {
                nodoActual.Derecho = InsertarRecursivo(nodoActual.Derecho, valor);
            }
            // Si es igual, no se hace nada (evita duplicados)

            return nodoActual;
        }

        // ---------------------- BUSCAR ----------------------

        /// <summary>
        /// Busca un valor dentro del arbol.
        /// </summary>
        public bool Buscar(int valor)
        {
            return BuscarRecursivo(raiz, valor);
        }

        private bool BuscarRecursivo(Nodo nodoActual, int valor)
        {
            if (nodoActual == null)
            {
                return false;
            }
            if (valor == nodoActual.Valor)
            {
                return true;
            }
            if (valor < nodoActual.Valor)
            {
                return BuscarRecursivo(nodoActual.Izquierdo, valor);
            }
            else
            {
                return BuscarRecursivo(nodoActual.Derecho, valor);
            }
        }

        // ---------------------- RECORRIDOS ----------------------

        /// <summary>Recorrido Inorden: Izquierda - Raiz - Derecha</summary>
        public string Inorden()
        {
            StringBuilder sb = new StringBuilder();
            InordenRecursivo(raiz, sb);
            return FormatearResultado(sb);
        }

        private void InordenRecursivo(Nodo nodoActual, StringBuilder sb)
        {
            if (nodoActual != null)
            {
                InordenRecursivo(nodoActual.Izquierdo, sb);
                sb.Append(nodoActual.Valor).Append(" ");
                InordenRecursivo(nodoActual.Derecho, sb);
            }
        }

        /// <summary>Recorrido Preorden: Raiz - Izquierda - Derecha</summary>
        public string Preorden()
        {
            StringBuilder sb = new StringBuilder();
            PreordenRecursivo(raiz, sb);
            return FormatearResultado(sb);
        }

        private void PreordenRecursivo(Nodo nodoActual, StringBuilder sb)
        {
            if (nodoActual != null)
            {
                sb.Append(nodoActual.Valor).Append(" ");
                PreordenRecursivo(nodoActual.Izquierdo, sb);
                PreordenRecursivo(nodoActual.Derecho, sb);
            }
        }

        /// <summary>Recorrido Postorden: Izquierda - Derecha - Raiz</summary>
        public string Postorden()
        {
            StringBuilder sb = new StringBuilder();
            PostordenRecursivo(raiz, sb);
            return FormatearResultado(sb);
        }

        private void PostordenRecursivo(Nodo nodoActual, StringBuilder sb)
        {
            if (nodoActual != null)
            {
                PostordenRecursivo(nodoActual.Izquierdo, sb);
                PostordenRecursivo(nodoActual.Derecho, sb);
                sb.Append(nodoActual.Valor).Append(" ");
            }
        }

        private string FormatearResultado(StringBuilder sb)
        {
            if (sb.Length == 0)
            {
                return "El arbol se encuentra vacio.";
            }
            return sb.ToString().Trim();
        }

        public bool EstaVacio()
        {
            return raiz == null;
        }
    }

    /// <summary>
    /// Clase principal del programa: contiene el menu y la interaccion con el usuario.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ArbolBinarioBusqueda arbol = new ArbolBinarioBusqueda();
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                MostrarEncabezado();
                MostrarMenu();

                int opcion = LeerEntero("Seleccione una opcion: ");

                switch (opcion)
                {
                    case 1:
                        OpcionInsertar(arbol);
                        break;
                    case 2:
                        OpcionBuscar(arbol);
                        break;
                    case 3:
                        Console.WriteLine("\nRecorrido Inorden (Izquierda - Raiz - Derecha):");
                        Console.WriteLine(arbol.Inorden());
                        break;
                    case 4:
                        Console.WriteLine("\nRecorrido Preorden (Raiz - Izquierda - Derecha):");
                        Console.WriteLine(arbol.Preorden());
                        break;
                    case 5:
                        Console.WriteLine("\nRecorrido Postorden (Izquierda - Derecha - Raiz):");
                        Console.WriteLine(arbol.Postorden());
                        break;
                    case 6:
                        continuar = false;
                        Console.Clear();
                        Console.WriteLine("Saliendo del programa. Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("\nOpcion invalida. Por favor, seleccione una opcion del 1 al 6.");
                        break;
                }

                if (continuar)
                {
                    Console.WriteLine("\nPresione ENTER para volver al menu...");
                    Console.ReadLine();
                }
            }
        }

        static void MostrarEncabezado()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("   ADMINISTRADOR DE ARBOL BINARIO DE BUSQUEDA");
            Console.WriteLine("            BST_Bryan_Yantuche");
            Console.WriteLine("==================================================");
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine("MENU PRINCIPAL");
            Console.WriteLine("1. Insertar Nodo");
            Console.WriteLine("2. Buscar Nodo");
            Console.WriteLine("3. Mostrar Recorrido Inorden");
            Console.WriteLine("4. Mostrar Recorrido Preorden");
            Console.WriteLine("5. Mostrar Recorrido Postorden");
            Console.WriteLine("6. Salir");
            Console.WriteLine("--------------------------------------------------");
        }

        static void OpcionInsertar(ArbolBinarioBusqueda arbol)
        {
            int valor = LeerEntero("\nIngrese el numero entero a insertar: ");
            bool insertado = arbol.Insertar(valor);

            if (insertado)
            {
                Console.WriteLine("El valor " + valor + " fue insertado correctamente en el arbol.");
            }
            else
            {
                Console.WriteLine("El valor " + valor + " ya existe en el arbol. No se permiten duplicados.");
            }
        }

        static void OpcionBuscar(ArbolBinarioBusqueda arbol)
        {
            if (arbol.EstaVacio())
            {
                Console.WriteLine("\nEl arbol se encuentra vacio. No hay valores para buscar.");
                return;
            }

            int valor = LeerEntero("\nIngrese el numero entero a buscar: ");
            bool encontrado = arbol.Buscar(valor);

            if (encontrado)
            {
                Console.WriteLine("El valor " + valor + " SI se encuentra en el arbol.");
            }
            else
            {
                Console.WriteLine("El valor " + valor + " NO se encuentra en el arbol.");
            }
        }

        /// <summary>
        /// Lee un numero entero de forma segura, validando la entrada del usuario.
        /// </summary>
        static int LeerEntero(string mensaje)
        {
            int valor;
            bool valido = false;
            do
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();
                valido = int.TryParse(entrada, out valor);

                if (!valido)
                {
                    Console.WriteLine("Entrada invalida. Por favor, ingrese un numero entero valido.");
                }
            } while (!valido);

            return valor;
        }
    }
}