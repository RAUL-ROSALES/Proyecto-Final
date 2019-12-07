using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Examen_6
{
    public class Operaciones
    {
        public List<Persona> TrabajarPersona;
        public Operaciones()
        {
            /*sirve para que la lista se inicialize desde el pricipio del programa y sea posibre visualizarlas*/
            TrabajarPersona = ObtenerPersona();
        }
        public void Menu()
        {
            Console.WriteLine("Bienvenid@");
            Console.WriteLine("Presione cualquier Tecla");
            Console.ReadKey();
            Console.Clear();
            try//en caso de que el usuario use sintaxis indebida
            {
                Console.WriteLine("------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Black;
                MostrarPersona();//metodo de despliegue de la lista de objetos
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Elija una opcion");
                Console.WriteLine("1.-Detalles de persona");//despliegue del menu
                Console.WriteLine("2.-Salir");
                switch (int.Parse(Console.ReadLine()))//capturas directo sin crear variable
                {
                    case 1://si el usuario quiere detallar ira al metodo despues de limpiar
                        Console.Clear();
                        DetallesPesona();//metodo para acceder al detallado de objeto
                        break;
                    case 2:
                        System.Environment.Exit(-1);//si el usuario quiere salir con esto se va
                        break;
                    default://un default para cuando el tipo de dato es valido pero no esta dentro del rango de opciones validas
                        Console.Clear();
                        Console.WriteLine("Seleccione una opcion valida, por favor.");
                        Console.ReadKey();
                        Console.Clear();
                        Menu();//limpia y te manda al menu 
                        break;
                }
            }
            finally { }          
        }
        public void MostrarPersona() //metodo para desplegar la lista publica
        {
            Console.WriteLine("Estos son las personas  que hay en la lista: ");
            foreach (var i in TrabajarPersona)//foreach para cada elemento de la lista
            {
                Console.WriteLine("Id: {0} - Nombre: {1}", i.Id, i.Nombre);//solo desplegamos id y nombre para saber como buscar el objeto deseado
            }
        }
        public List<string> ObtenerLineas(string Ruta)//es metodo tipo lista de strings porque es pasajero, aqui se saca la info del txt
        {
            List<string> lineas = new List<string>();//creas tu lista
            if (File.Exists(Ruta))//buscas en el file si existe
            {
                string[] datos = File.ReadAllLines(Ruta);//creas un array de datos que sacara su info del txt
                foreach (var i in datos)//foreach para buscar en el array
                {
                    lineas.Add(i);//por cada elemento que este en el array, se agrega a la lista de strings
                }
            }
            else
            {
                Console.WriteLine("archivo no existente");//si no hay archivo se devuelve un null
                return null;
            }
            return lineas;//cuando se llene la lista de strings se devuelve al metodo que sigue
        }
        public List<Persona> ObtenerPersona()
        {
            var lineas = ObtenerLineas("Lista.txt");//jalo la lista 
            List<Persona> persona_1 = new List<Persona>();//creas lista de objetos
            foreach (var i in lineas)//foreach para buscar en la lista de strings
            {
                string[] datos = i.Split(',');//por cada elemento dentro de tu lsita creas un arreglo de cinco elementos, divididos por una coma
                persona_1.Add(new Persona { Id = int.Parse(datos[0]), Nombre = datos[1], Salario = datos[2], Area = datos[3], Puesto = datos[4] });//cada que llenes tu arreglo de 5 elementos, los conviertes en atributos del objeto y los agregas a la lista 
            }
            return persona_1;//devuelves la lista de objetos llenos, la cual es enviada al ctor y a su vez es global
        }
        public void DetallesPesona() //metodo para detallar un objeto
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Lista");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Black;
                MostrarPersona();//se le muestras lista para que el usuario sepa que busca
                Console.WriteLine("------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.White;
                Persona p = new Persona();//instancias para llenar y desplegar objeto
                Console.WriteLine("Seleccione a la persona por su 'ID':");
                int perosax = int.Parse(Console.ReadLine());//usas este id como tu elemento buscador
                foreach (var i in TrabajarPersona)//foreach para la busqueda
                {
                    if (perosax == i.Id)//si el elemento aparece en tu lsita
                    {
                        p = i;//conviertes a ese elemento en tu objeto a desplegar
                    }
                } 
                Console.WriteLine("Nombre:  {0}\nSalario:  {1}\nArea:  {2}\nPuesto:  {3}", p.Nombre, p.Salario,p.Area, p.Puesto);//desplegamos todos los atributos de modo normal pero en vez de usar el atributo element usamos el string que acabamos de crear y llenar con el switch*/
                Console.WriteLine("\nSi desea modificar algun dato presione 'si'");
                Console.WriteLine("en caso de querer volver a la lista presione 'volver'.");
                string opc = Console.ReadLine();//le decimos al usuario si quiere editar o no, con un try catch por si hay un error en la sintaxis
                opc = opc.ToLower();
                if (opc == "si")//si dice que si modificaremos el objeto aqui
                {
                    p = EditarPersona(p);//el objeto elegido a ver, sera sustituido por su nueva version que sera enviada al metodo de cambio como sobrecarga, como diria thanos, use las gemas para destruir las gemas, something like that
                    ActualizatTxt();//despues de modificar la lista se llama este metodo para actualizar el archivo txt
                    Console.Clear();//se limpia y se vuelve al menu
                    Menu();
                }
                else//si se niega a editar accedemos al menu de manera normal
                {
                    Console.ReadKey();
                    Console.Clear();
                    Menu();
                }
            }
            catch (Exception e) //en caso de excepcion se interrumpe la edicion del objeto
            {
                Console.WriteLine("Se ha detectado el error: {0}\nPresiona cualquier tecla para volver al menu", e.Message);
                Console.ReadKey();
                Console.Clear();
                Menu();
            }
        }
        public Persona EditarPersona(Persona p) //meetodo para cuando se acepte la edicion del objeto
        {
            try//en caso de error de sintaxis
            {
                Console.WriteLine("Seleccione una opcion");
                Console.WriteLine("1.-Nombre\n2.-Salario\n3.-Area\n4.-Puesto");
                int opc = Convert.ToInt32(Console.ReadLine());
                switch (opc)//con el numero se elige el atributo a modificar (solamente uno por vuelta)
                {
                    case 1:
                        Console.WriteLine("Ingrese un nuevo Nombre: ");
                        string nombre = Console.ReadLine();
                        p.Nombre = nombre;
                        Console.WriteLine("Se ha modificado el Nombre.");
                        break;
                    case 2:
                        Console.WriteLine("Ingrese Nuevo Salario: ");
                        string salario = Console.ReadLine();
                        p.Salario = salario;
                        Console.WriteLine("Se ha modificado el Nuevo Salario.");
                        break;
                    case 3:
                        Console.WriteLine("Ingrese nueva area de trabajo");
                        string area = Console.ReadLine();
                        p.Area = area;
                        Console.WriteLine("Se ha modificado el area de Trabajo.");
                        break;
                    case 4:
                        Console.WriteLine("Ingrese nueva puesto: ");
                        string puesto = Console.ReadLine();
                        p.Puesto = puesto;
                        Console.WriteLine("Se ha modificado el Nuevo puesto.");
                        break;
                    default: break;
                }
                Console.WriteLine("Presione ENTER para continuar");
                Console.ReadKey();
                return p;//se devuelve el objeto modificado
            }
            catch (Exception exe) //en caso de error de sintaxis el objeto se devolvera sin cambios (por eso se llena una variable primero)
            {
                Console.WriteLine("Se ha detectado el error: {0}\nSe regresara al menu", exe.Message);
                Menu();
                return p;
            }
        }
        public void ActualizatTxt()//metodo que se usa despues de modificar un objeto de una lista
        {//no es mas que todo el proceso de crear la lista global del constructor pero a la inversa, 
            List<string> lineas = new List<string>();//haces una lista de strings para fusionarla
            foreach (var persona in TrabajarPersona) //por cada elemento en tu lista de objetos
            {
                string[] atributos = new string[5];//llenas un vector de strings
                atributos[0] = Convert.ToString(persona.Id);
                atributos[1] = persona.Nombre;
                atributos[2] = persona.Salario;
                atributos[3] = persona.Area;
                atributos[4] = persona.Puesto;//el cual llenas de los atributos del objeto
                lineas.Add(string.Join(",", atributos));//luego para agregarlo a tu lista de strings usas el join en vez del split, aqui es la inversa
            }
            var joinedstring = string.Join("\n", lineas);//luego usas otro join para fusionar tu lista de strings, usas el backslash n para dividir tu string, en el join anterior usaste una coma, con esto cada renglon sera el objeto hecho string, en otras palabras cada elemento de la lista de strings que acabamos de hacer usara un renglon del txt
            File.WriteAllText("Lista.txt", joinedstring);//por ultimo solo hay que meter este gran string de 20 renglones al archivo txt, y con eso el programa queda completo
        }
    }
}
