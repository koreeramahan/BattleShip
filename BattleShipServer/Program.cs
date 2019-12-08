using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Sockets;

namespace BattleShipServer
{
    class Program
    {
        //список не играющих людей и их айпи
        public static Dictionary<string, Socket> loggedNicks = new Dictionary<string, Socket>();
        //список играющих людей и их айпи
        public static Dictionary<string, Socket> loggedplayingNicks = new Dictionary<string, Socket>();
        //человек и список людей, предлагающих игру
        public static Dictionary<string, List<string>> enemiesoffers = new Dictionary<string, List<string>>();
        //Form1 игроки нажали начать <кто+кому>
        public static List<string> whowhomSentStart = new List<string>();
        //Form1 игроки нажали сдаться
        public static List<string> whowhomSentGiveUp = new List<string>();
        static void Main(string[] args)
        {
            //нельзя начать сразу две игры
            String thisprocessname = Process.GetCurrentProcess().ProcessName;

            if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
                return;

            AsynchronousSocketListener.StartListening();
        }
    }
   
}
