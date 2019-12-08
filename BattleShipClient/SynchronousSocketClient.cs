using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShipClient
{
    class SynchronousSocketClient
    {
        //сокет для соединения с сервером
        public Socket socket;
        //буфер данных для приходящих с сервера сообщений
        byte[] bytes;
        //буфер для данных приходящих от врагов
        private byte[] byteData = new byte[1024];
        //установили корабли и нажали кнопку готовности
        bool iAmReady = false;
        //если игрок уже играет
        bool iamBusy = false; 
        public SynchronousSocketClient(string AddressIP)
        {
            IPEndPoint serverRemoteEP = new IPEndPoint(IPAddress.Parse(AddressIP), 11000);

            //создать TCP/IP сокет 
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //установить соединение с удаленной точкой. проверить ошибки
            try
            {
                socket.Connect(serverRemoteEP);
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                throw;
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
                throw;
            }
        }
        public string Receive()
        {
            bytes = new byte[1024];
            int bytesRec = 0;
            string answer = string.Empty;
            try
            {
                while (!answer.Contains("<EOF>"))
                {
                    //получить ответ от другого устройства
                    bytesRec = socket.Receive(bytes);
                    answer += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                }
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                throw;
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
                throw;
            }
            return answer;
        }
        public void Send(String data)
        {
            try
            {
                //преобразовать строчку данных в массив байтов
                byte[] msg = Encoding.ASCII.GetBytes(data);

                //отправить данные через сокет
                int bytesSent = socket.Send(msg);
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                throw;
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
                throw;
            }
        }

        public void Disconnect()
        {
            try
            {
                //освободить сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                throw;
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
                throw;
            }
        }
        //сообщения был отправлены
    }
}
