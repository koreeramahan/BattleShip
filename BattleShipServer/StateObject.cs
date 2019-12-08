using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace BattleShipServer
{
    //https://msdn.microsoft.com/pl-pl/library/fx6588te(v=vs.110).aspx
    // для считывания асинхронных данных клиента
    class StateObject
    {
        //клиент сокет
        public Socket workSocket = null;
        //размер полученного буфера
        public const int BufferSize = 1024;
        //получить буфер
        public byte[] buffer = new byte[BufferSize];
        //полученная строка данных
        public StringBuilder sb = new StringBuilder();
    }
}
