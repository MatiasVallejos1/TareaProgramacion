using HolaMundoSocket.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HolaMundoSocket
{
    class Program
    {
        private static void GenerarChat(ClienteCom cliente)
        {
            bool terminar = false;
            String respuesta, mensaje;
            while (!terminar)
            {
                mensaje = cliente.Leer();
                if (mensaje == null)
                {
                    Console.WriteLine("...");
                }
                else
                {
                    Console.WriteLine("El cliente Dice: {0}", mensaje);
                    if (mensaje.ToLower().Equals("chao"))
                    {
                        cliente.Desconectar();
                        terminar = true;
                    }
                }
                respuesta = Console.ReadLine().Trim();
                cliente.Escribir(respuesta);
                if (respuesta.ToLower().Equals("chao"))
                {
                    cliente.Desconectar();
                    terminar = true;
                }
            }
        }
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);

            Console.WriteLine("Iniciando Servidor en puerto {0}", puerto);

            ServerSocket servidor = new ServerSocket(puerto);

                if (servidor.Iniciar())
                {
                    //Ok pude conectar
                    Console.WriteLine("Sevidor iniciado");

                    while (true)
                    {
                    Console.WriteLine("Esperando Cliente");
                    Socket socketCliente = servidor.ObtenerCliente();
                    ClienteCom cliente = new ClienteCom(socketCliente);
                    GenerarChat(cliente);
                    
                    }
                }
                    Console.WriteLine("Error, el puerto {0} esta en uso", puerto);
                }
            }
        }
    

