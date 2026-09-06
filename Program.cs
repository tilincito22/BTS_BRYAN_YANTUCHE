// Laboratorio 3 - Árbol AVL en C#
// Estudiante: Bryan Yantuche
// Procesos y Algoritmos II

using System;
using System.Text.RegularExpressions;

namespace AVL_Bryan_Yantuche
{
    // clase para guardar los datos de un paciente
    public class Expediente
    {
        public string NumeroExpediente;
        public string NombrePaciente;
        public int Edad;
        public string TipoSangre;

        public Expediente(string numero, string nombre, int edad, string tipoSangre)
        {
            NumeroExpediente = numero;
            NombrePaciente = nombre;
            Edad = edad;
            TipoSangre = tipoSangre;
        }

        public void Mostrar()
        {
            Console.WriteLine("Número de Expediente : " + NumeroExpediente);
            Console.WriteLine("Nombre del Paciente  : " + NombrePaciente);
            Console.WriteLine("Edad                 : " + Edad + " años");
            Console.WriteLine("Tipo de Sangre       : " + TipoSangre);
        }
    }

    // nodo del arbol, cada nodo tiene su expediente y sus 2 hijos
    public class Nodo
    {
        public Expediente Dato;
        public Nodo Izq;
        public Nodo Der;
        public int Altura;

        public Nodo(Expediente dato)
        {
            Dato = dato;
            Izq = null;
            Der = null;
            Altura = 1; // cuando se crea es hoja, altura 1
        }
    }

    // aqui va toda la logica del AVL
    public class ArbolAVL
    {
        private Nodo raiz = null;

        public bool EstaVacio()
        {
            return raiz == null;
        }

        // saca la altura de un nodo, si es null la altura es 0
        private int Altura(Nodo n)
        {
            if (n == null) return 0;
            return n.Altura;
        }

        // el factor de balance es la resta de las alturas izq y der
        private int Balance(Nodo n)
        {
            if (n == null) return 0;
            return Altura(n.Izq) - Altura(n.Der);
        }

        private void ActualizarAltura(Nodo n)
        {
            int izq = Altura(n.Izq);
            int der = Altura(n.Der);
            n.Altura = 1 + Math.Max(izq, der);
        }

        // rotacion simple a la derecha (caso LL)
        private Nodo RotarDerecha(Nodo y)
        {
            Nodo x = y.Izq;
            Nodo temp = x.Der;

            x.Der = y;
            y.Izq = temp;

            ActualizarAltura(y);
            ActualizarAltura(x);

            return x;
        }

        // rotacion simple a la izquierda (caso RR)
        private Nodo RotarIzquierda(Nodo x)
        {
            Nodo y = x.Der;
            Nodo temp = y.Izq;

            y.Izq = x;
            x.Der = temp;

            ActualizarAltura(x);
            ActualizarAltura(y);

            return y;
        }

        public void Insertar(Expediente exp)
        {
            raiz = Insertar(raiz, exp);
        }

        private Nodo Insertar(Nodo nodo, Expediente exp)
        {
            // caso base, aqui se inserta el nodo nuevo
            if (nodo == null)
                return new Nodo(exp);

            int cmp = string.Compare(exp.NumeroExpediente, nodo.Dato.NumeroExpediente);

            if (cmp < 0)
                nodo.Izq = Insertar(nodo.Izq, exp);
            else if (cmp > 0)
                nodo.Der = Insertar(nodo.Der, exp);
            else
                throw new Exception("Ya existe ese expediente");

            // actualizo altura del nodo actual
            ActualizarAltura(nodo);

            // reviso si se desbalanceo
            int bal = Balance(nodo);

            // caso LL
            if (bal > 1 && string.Compare(exp.NumeroExpediente, nodo.Izq.Dato.NumeroExpediente) < 0)
                return RotarDerecha(nodo);

            // caso RR
            if (bal < -1 && string.Compare(exp.NumeroExpediente, nodo.Der.Dato.NumeroExpediente) > 0)
                return RotarIzquierda(nodo);

            // caso LR
            if (bal > 1 && string.Compare(exp.NumeroExpediente, nodo.Izq.Dato.NumeroExpediente) > 0)
            {
                nodo.Izq = RotarIzquierda(nodo.Izq);
                return RotarDerecha(nodo);
            }

            // caso RL
            if (bal < -1 && string.Compare(exp.NumeroExpediente, nodo.Der.Dato.NumeroExpediente) < 0)
            {
                nodo.Der = RotarDerecha(nodo.Der);
                return RotarIzquierda(nodo);
            }

            // si no se desbalanceo se regresa tal cual
            return nodo;
        }

        public Expediente Buscar(string numero)
        {
            return Buscar(raiz, numero);
        }

        private Expediente Buscar(Nodo nodo, string numero)
        {
            if (nodo == null) return null;

            int cmp = string.Compare(numero, nodo.Dato.NumeroExpediente);

            if (cmp == 0) return nodo.Dato;
            if (cmp < 0) return Buscar(nodo.Izq, numero);
            return Buscar(nodo.Der, numero);
        }

        public bool Existe(string numero)
        {
            return Buscar(numero) != null;
        }

        // recorridos, se imprimen directo, no se guardan en lista

        public void Inorden()
        {
            if (EstaVacio())
            {
                Console.WriteLine("El árbol está vacío");
                return;
            }
            InordenRec(raiz);
        }
        private void InordenRec(Nodo nodo)
        {
            if (nodo == null) return;
            InordenRec(nodo.Izq);
            Console.WriteLine(nodo.Dato.NumeroExpediente);
            InordenRec(nodo.Der);
        }

        public void Preorden()
        {
            if (EstaVacio())
            {
                Console.WriteLine("El árbol está vacío");
                return;
            }
            PreordenRec(raiz);
        }
        private void PreordenRec(Nodo nodo)
        {
            if (nodo == null) return;
            Console.WriteLine(nodo.Dato.NumeroExpediente);
            PreordenRec(nodo.Izq);
            PreordenRec(nodo.Der);
        }

        public void Postorden()
        {
            if (EstaVacio())
            {
                Console.WriteLine("El árbol está vacío");
                return;
            }
            PostordenRec(raiz);
        }
        private void PostordenRec(Nodo nodo)
        {
            if (nodo == null) return;
            PostordenRec(nodo.Izq);
            PostordenRec(nodo.Der);
            Console.WriteLine(nodo.Dato.NumeroExpediente);
        }

        public int AlturaTotal()
        {
            return Altura(raiz);
        }
    }

    class Program
    {
        static ArbolAVL arbol = new ArbolAVL();
        static Regex formato = new Regex(@"^EXP\d{4}$");
        static string[] tiposSangre = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };

        static void Main(string[] args)
        {
            bool seguir = true;

            while (seguir)
            {
                Console.WriteLine("=================================================");
                Console.WriteLine("   SISTEMA DE GESTIÓN DE EXPEDIENTES MÉDICOS");
                Console.WriteLine("               ÁRBOL AVL");
                Console.WriteLine("=================================================");
                Console.WriteLine();
                Console.WriteLine("[1] Registrar Expediente");
                Console.WriteLine("[2] Buscar Expediente");
                Console.WriteLine("[3] Mostrar Recorrido Inorden");
                Console.WriteLine("[4] Mostrar Recorrido Preorden");
                Console.WriteLine("[5] Mostrar Recorrido Postorden");
                Console.WriteLine("[6] Mostrar Altura del Árbol");
                Console.WriteLine("[7] Salir");
                Console.WriteLine();
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                if (opcion == "1")
                {
                    Registrar();
                }
                else if (opcion == "2")
                {
                    Buscar();
                }
                else if (opcion == "3")
                {
                    Console.WriteLine("RECORRIDO INORDEN");
                    Console.WriteLine("-----------------");
                    arbol.Inorden();
                }
                else if (opcion == "4")
                {
                    Console.WriteLine("RECORRIDO PREORDEN");
                    Console.WriteLine("------------------");
                    arbol.Preorden();
                }
                else if (opcion == "5")
                {
                    Console.WriteLine("RECORRIDO POSTORDEN");
                    Console.WriteLine("-------------------");
                    arbol.Postorden();
                }
                else if (opcion == "6")
                {
                    Console.WriteLine("ALTURA DEL ÁRBOL");
                    Console.WriteLine("----------------");
                    Console.WriteLine("La altura actual del árbol es: " + arbol.AlturaTotal());
                }
                else if (opcion == "7")
                {
                    seguir = false;
                    Console.WriteLine("Chao, gracias por usar el sistema.");
                }
                else
                {
                    Console.WriteLine("Esa opción no existe, intenta de nuevo.");
                }

                if (seguir)
                {
                    Console.WriteLine();
                    Console.WriteLine("Presiona una tecla para volver al menú...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void Registrar()
        {
            Console.WriteLine("REGISTRO DE EXPEDIENTE");
            Console.WriteLine("-----------------------");

            string numero;
            while (true)
            {
                Console.Write("Número de Expediente (EXPxxxx): ");
                numero = Console.ReadLine().Trim().ToUpper();

                if (!formato.IsMatch(numero))
                {
                    Console.WriteLine("Formato mal, debe ser EXP y 4 numeros, ej EXP0001");
                    continue;
                }
                if (arbol.Existe(numero))
                {
                    Console.WriteLine("Ese expediente ya existe, prueba con otro numero");
                    continue;
                }
                break;
            }

            Console.Write("Nombre del Paciente: ");
            string nombre = Console.ReadLine().Trim();

            int edad;
            while (true)
            {
                Console.Write("Edad: ");
                if (int.TryParse(Console.ReadLine(), out edad) && edad >= 0 && edad <= 120)
                    break;
                Console.WriteLine("Edad invalida, pon un numero entre 0 y 120");
            }

            string tipoSangre;
            while (true)
            {
                Console.Write("Tipo de Sangre (A+, A-, B+, B-, AB+, AB-, O+, O-): ");
                tipoSangre = Console.ReadLine().Trim().ToUpper();

                bool ok = false;
                foreach (string t in tiposSangre)
                {
                    if (t == tipoSangre) { ok = true; break; }
                }
                if (ok) break;
                Console.WriteLine("Ese tipo de sangre no existe, intenta otra vez");
            }

            Expediente nuevo = new Expediente(numero, nombre, edad, tipoSangre);
            arbol.Insertar(nuevo);

            Console.WriteLine();
            Console.WriteLine("Expediente guardado:");
            nuevo.Mostrar();
        }

        static void Buscar()
        {
            Console.WriteLine("BÚSQUEDA DE EXPEDIENTE");
            Console.WriteLine("-----------------------");

            Console.Write("Ingrese el número de expediente: ");
            string numero = Console.ReadLine().Trim().ToUpper();

            Console.WriteLine();
            Expediente encontrado = arbol.Buscar(numero);

            if (encontrado != null)
                encontrado.Mostrar();
            else
                Console.WriteLine("No se encontró ese expediente");
        }
    }
}