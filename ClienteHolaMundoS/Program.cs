using ClienteHolaMundoS.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteHolaMundoS
{
    class Program
    {
        private static void GenerarChat(ClienteSocket clienteSocket)
        {
            bool terminar = false;
            String respuesta, mensaje;
            while (!terminar)
            {
                respuesta = Console.ReadLine().Trim();
                if (!respuesta.Equals(""))
                {
                    clienteSocket.Escribir(respuesta);
                    if (respuesta.ToLower().Equals("chao"))
                    {
                        clienteSocket.Desconectar();
                        terminar = true;
                    }
                }
                mensaje = clienteSocket.Leer();
                if (mensaje == null)
                {
                    Console.WriteLine("Conexion finalizada...");
                }
                else
                {
                    Console.WriteLine("El servidor dice: {0}", mensaje);
                    
                }
                
            }

        }
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            string servidor = ConfigurationManager.AppSettings["servidor"];

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conectando a servidor {0} en puerto {1}", servidor, puerto);
            ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);
            if (clienteSocket.Conectar())
            {
                Console.WriteLine("Conectado...");
                GenerarChat(clienteSocket);
            }
            else
            {
                Console.WriteLine("Error de comunicacion...");
            }
            Console.ReadKey();
            

        }
    }
}
