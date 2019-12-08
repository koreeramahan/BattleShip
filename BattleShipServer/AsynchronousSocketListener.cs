using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace BattleShipServer
{
    //https://msdn.microsoft.com/pl-pl/library/fx6588te(v=vs.110).aspx
    class AsynchronousSocketListener
    {
        //потоковый сигнал
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public AsynchronousSocketListener() { }

        public static void StartListening()
        {
            //буфер данных для приходящих данных
            byte[] bytes = new Byte[1024];

            //установить локальный эндпоинт для сокета
            //DNS имя компьютера
            //запустить прослушиватель
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            int i = 0;
            Console.WriteLine("Выберите IP адрес сервера:");
            foreach (var item in ipHostInfo.AddressList)
            {
                Console.WriteLine(i + " - " + item);
                i++;
            }
            do
            {
                Console.Write("Тип: ");
                i = (int)(Console.ReadKey().KeyChar) - 48;
                Console.WriteLine();
            } while (i < 0 || i >= ipHostInfo.AddressList.Count());

            IPAddress ipAddress = ipHostInfo.AddressList[i];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            //создать TCP/IP сокет
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            //прикрепить сокет к локальному эндпоинту и слушать приходящие соединения
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                Console.WriteLine("Сервер запущен (" + ipAddress + ")");
                while (true)
                {
                    //установить событие в nonsignaled состояние
                    allDone.Reset();

                    //асинхронный сокет начинает прослушивание соединений               
                    listener.BeginAccept(new AsyncCallback(AcceptCallback),listener);

                    //подождать пока установится соединения, прежде чем продолжить
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nНажмите ENTER для продолжения...");
            Console.Read();

        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // сигнал для продолжения
            allDone.Set();

            //Получить сокет, который управляет запросом клиента
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            //создать стейт обжект
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            //получить состояние и сокет обработчика
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            int bytesRead = 0;

            //считать данных с клиент сокета
            bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                //хранение полученных данных
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                //проверить end-of-file тег. если фолс, считывать больше данных
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    //все данные были считаны с клиента, отправить об этом сообщение
                    Console.WriteLine("Считано {0} байт из сокета. \nДанные : {1}",
                        content.Length, content);

                    string messageBits = Utilities.getBinaryMessage(content);
                    //взять 8 бит, чтобы распознать запрос
                    int bits8 = Convert.ToInt32(messageBits.Substring(0, 8), 2);
                    //получить параметры, отправленные с сообщением
                    //parameters[0] is message flag
                    string[] parameters = content.Split(' ');
                    string nick = String.Empty;
                    string IPport = String.Empty;
                    string port = String.Empty;
                    string enemyNick = String.Empty;
                    string players = String.Empty;
                    bool result = false;
                    string whoSent = "";
                    string whomSent = "";
                    switch (bits8)
                    {
                        case 0://начать игру
                            {
                                //получить ник
                                whoSent = parameters[1];
                                //получить ник
                                whomSent = parameters[2];

                                if (Program.loggedplayingNicks.ContainsKey(whomSent))
                                {
                                    //проверить, кто отправил сообщение ранее

                                    //проверить whom+who на whowhomSentStart & whowhomSentGiveUp
                                    if (!Program.whowhomSentStart.Contains(whomSent + whoSent) && !Program.whowhomSentGiveUp.Contains(whomSent + whoSent))
                                    {
                                        Program.whowhomSentStart.Add(whoSent + whomSent);
                                        state.buffer = new byte[1024];
                                        state.sb = new StringBuilder();
                                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                        new AsyncCallback(ReadCallback), state);
                                        break;
                                    }
                                    else if (Program.whowhomSentStart.Contains(whomSent + whoSent))//проверить whom+who на whowhomSentStart
                                    {
                                        //отправить ок обоим игрокам
                                        if (Program.loggedplayingNicks.ContainsKey(whoSent))
                                        {
                                            Send(Program.loggedplayingNicks[whoSent], ((char)16).ToString() + " <EOF>");
                                        }
                                        if (Program.loggedplayingNicks.ContainsKey(whomSent))
                                        {
                                            Send(Program.loggedplayingNicks[whomSent], ((char)0).ToString() + " <EOF>");
                                        }
                                        //убрать обоих игроков с whowhomSentStart
                                        Program.whowhomSentStart.Remove(whomSent + whoSent);
                                        state.buffer = new byte[1024];
                                        state.sb = new StringBuilder();
                                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                        new AsyncCallback(ReadCallback), state);
                                        break;

                                    }
                                    else if (Program.whowhomSentGiveUp.Contains(whomSent + whoSent))//проверить whom+who на whowhomSentGiveUp
                                    {
                                        if (Program.loggedplayingNicks.ContainsKey(whoSent))
                                        {
                                            Send(Program.loggedplayingNicks[whoSent], ((char)17).ToString() + " <EOF>");
                                        }
                                        Program.whowhomSentGiveUp.Remove(whomSent + whoSent);
                                    }
                                }
                                else if (Program.loggedplayingNicks.ContainsKey(whoSent))
                                {
                                    Send(Program.loggedplayingNicks[whoSent], ((char)17).ToString() + " <EOF>");
                                    Program.loggedNicks.Add(whoSent, Program.loggedplayingNicks[whoSent]);
                                    Program.loggedplayingNicks.Remove(whoSent);
                                }
                                else if (Program.loggedNicks.ContainsKey(whoSent))
                                {
                                    Send(Program.loggedNicks[whoSent], ((char)17).ToString() + " <EOF>");
                                }
                                state.buffer = new byte[1024];
                                state.sb = new StringBuilder();
                                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReadCallback), state);
                                break;
                            }
                        case 1://закончить игру
                            {
                                nick = parameters[1];
                                enemyNick = parameters[2];

                                if (Program.loggedplayingNicks.ContainsKey(nick))
                                {
                                    if (!Program.loggedNicks.ContainsKey(nick))
                                    {
                                        Program.loggedNicks.Add(nick, Program.loggedplayingNicks[nick]);
                                        Program.loggedplayingNicks.Remove(nick);
                                    }
                                }
                                if (Program.loggedplayingNicks.ContainsKey(enemyNick))
                                {
                                    if (!Program.loggedNicks.ContainsKey(enemyNick))
                                    {
                                        Program.loggedNicks.Add(enemyNick, Program.loggedplayingNicks[enemyNick]);
                                        Program.loggedplayingNicks.Remove(enemyNick);
                                        Send(Program.loggedNicks[enemyNick], ((char)1).ToString() + " <EOF>");
                                    }
                                }
                                state.buffer = new byte[1024];
                                state.sb = new StringBuilder();
                                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReadCallback), state);
                                break;
                            }
                        case 2: //сдаться
                            { 
                                //получить ник
                                whoSent = parameters[1];
                                //получить ник
                                whomSent = parameters[2];
                                //проверить whomSent отправил сообщение раньше
                               
                                //проверить whom+who на whowhomSentStart & whowhomSentGiveUp
                                if (!Program.whowhomSentStart.Contains(whomSent + whoSent) && !Program.whowhomSentGiveUp.Contains(whomSent + whoSent))
                                {
                                    Program.whowhomSentGiveUp.Add(whoSent + whomSent);
                                    if (Program.loggedplayingNicks.ContainsKey(whoSent))
                                    {
                                        if (!Program.loggedNicks.ContainsKey(whoSent))
                                        {
                                            Program.loggedNicks.Add(whoSent, Program.loggedplayingNicks[whoSent]);
                                            Program.loggedplayingNicks.Remove(whoSent);
                                            Send(Program.loggedNicks[whoSent], ((char)10).ToString() + " <EOF>");
                                        }
                                    }
                                    state.buffer = new byte[1024];
                                    state.sb = new StringBuilder();
                                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                    new AsyncCallback(ReadCallback), state);
                                    break;
                                }
                                else if (Program.whowhomSentStart.Contains(whomSent + whoSent))//проверить whom+who на whowhomSentStart
                                {
                                    //отправить Fail whom игроку
                                    if (Program.loggedplayingNicks.ContainsKey(whomSent))
                                    {
                                        Send(Program.loggedplayingNicks[whomSent], ((char)9).ToString() + " <EOF>");
                                    }
                                    //убрать обоих игроков с whowhomSentStart
                                    Program.whowhomSentStart.Remove(whomSent + whoSent);

                                    //убрать обоих игроков с loggedplayingNicks
                                    if (Program.loggedplayingNicks.ContainsKey(whomSent))
                                    {
                                        if (!Program.loggedNicks.ContainsKey(whomSent))
                                        {
                                            Program.loggedNicks.Add(whoSent, Program.loggedplayingNicks[whomSent]);
                                            Program.loggedplayingNicks.Remove(whomSent);                                         
                                        }
                                    }
                                    if (Program.loggedplayingNicks.ContainsKey(whoSent))
                                    {
                                        if (!Program.loggedNicks.ContainsKey(whoSent))
                                        {
                                            Program.loggedNicks.Add(whoSent, Program.loggedplayingNicks[whoSent]);
                                            Program.loggedplayingNicks.Remove(whoSent);
                                            Send(Program.loggedNicks[whoSent], ((char)17).ToString() + " <EOF>");
                                        }
                                    }
                                    state.buffer = new byte[1024];
                                    state.sb = new StringBuilder();
                                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                    new AsyncCallback(ReadCallback), state);
                                    break;
                                }
                                else if (Program.whowhomSentGiveUp.Contains(whomSent + whoSent))//проверить whom+who на whowhomSentGiveUp
                                {
                                    Program.whowhomSentGiveUp.Remove(whomSent + whoSent);
                                    if (Program.loggedplayingNicks.ContainsKey(whoSent))
                                    {
                                        if (!Program.loggedNicks.ContainsKey(whoSent))
                                        {
                                            Program.loggedNicks.Add(whoSent, Program.loggedplayingNicks[whoSent]);
                                            Program.loggedplayingNicks.Remove(whoSent);
                                            Send(Program.loggedNicks[whoSent], ((char)17).ToString() + " <EOF>");
                                        }
                                        
                                    }
                                }
                                state.buffer = new byte[1024];
                                state.sb = new StringBuilder();
                                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReadCallback), state);
                                break;
                            }
                        case 3: //потопленный корабль
                            {


                                break;
                            }
                        case 4: //промах
                            {
                                enemyNick = parameters[1];
                                if (Program.loggedplayingNicks.ContainsKey(enemyNick))
                                {
                                    string message = ((char)4).ToString() + " <EOF>";
                                    Send(Program.loggedplayingNicks[enemyNick], message);
                                }
                                state.buffer = new byte[1024];
                                state.sb = new StringBuilder();
                                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReadCallback), state);
                                break;
                            }
                        case 5: //попал
                            {
                                enemyNick = parameters[1];
                                if (Program.loggedplayingNicks.ContainsKey(enemyNick))
                                {
                                    string message = ((char)5).ToString() +  " <EOF>";
                                    Send(Program.loggedplayingNicks[enemyNick], message);
                                }
                                state.buffer = new byte[1024];
                                state.sb = new StringBuilder();
                                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReadCallback), state);
                                break;
                            }
                        case 6://выстрел
                            {
                                enemyNick = parameters[1];
                                string x = parameters[2];
                                string y = parameters[3];
                                if (Program.loggedplayingNicks.ContainsKey(enemyNick))
                                {
                                    string message = ((char)6).ToString() +" " + x +" " + y + " <EOF>";
                                    Send(Program.loggedplayingNicks[enemyNick], message);
                                }
                                state.buffer = new byte[1024];
                                state.sb = new StringBuilder();
                                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReadCallback), state);
                                break;
                            }
                        case 7://GetOffers - кто-то хочет знать, кто предлагает ему играть
                            {
                                //получить ник
                                nick = parameters[1];
                                //предложения соперников
                                if (Program.enemiesoffers.ContainsKey(nick))
                                {
                                    //враг1, враг2, враг3..
                                    string enemiesString ="";
                                    foreach (var item in Program.enemiesoffers[nick])
                                    {
                                        enemiesString += item + " ";
                                    }
                                    string message = ((char)7).ToString() + " " + enemiesString + "<EOF>";
                                    Send(handler, message);
                                }
                                else
                                {
                                    Send(handler, ((char)7).ToString() + " <EOF>");
                                }
                                state.buffer = new byte[1024];
                                state.sb = new StringBuilder();
                                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReadCallback), state);
                                break;
                            }
                        case 8://Offer - кто-то предлагает кому-то игру
                            {
                                //кто предлагает (enemy)
                                nick = parameters[1];
                                //кому предлагает
                                enemyNick = parameters[2];
                                bool nickOffers = false;
                                if (Program.enemiesoffers.ContainsKey(nick))
                                {
                                    if (Program.enemiesoffers[nick].Contains(enemyNick))
                                    {
                                        Send(handler, ((char)10).ToString() + " <EOF>");
                                        nickOffers = true;
                                    }
                                }
                                if (nickOffers == false)
                                {
                                    if (Program.enemiesoffers.ContainsKey(enemyNick))
                                    {
                                        Program.enemiesoffers[enemyNick].Add(nick);
                                    }
                                    else
                                    {
                                        Program.enemiesoffers.Add(enemyNick, new List<string>() { nick });
                                    }
                                }
                                state.buffer = new byte[1024];
                                state.sb = new StringBuilder();
                                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReadCallback), state);
                                break;
                            }
                        case 9: //Fail
                            {
                                Send(handler, ((char)10).ToString() + " <EOF>");

                                state.buffer = new byte[1024];
                                state.sb = new StringBuilder();
                                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReadCallback), state);
                                break;
                            }
                        case 10: //OK
                            {
                                Send(handler, ((char)10).ToString() + " <EOF>");

                                state.buffer = new byte[1024];
                                state.sb = new StringBuilder();
                                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReadCallback), state);
                                break;
                            }
                        case 11: //присоединение (Соединение: клиент присоединяется к серверу) (TICK)
                            {
                                //получить ник
                                nick = parameters[1];
                                //получить айпи
                                IPport = handler.RemoteEndPoint.ToString().Split(':')[0];
                                //получить порт Нет
                                port = parameters[2];
                                IPport += ":" + port;
                                //проверить есть ли уже такой ник
                                if (Program.loggedNicks.ContainsKey(nick)) //ник занят
                                {
                                    //отправить Fail
                                    Send(handler, ((char)9).ToString() + " <EOF>");
                                }
                                else //пользователь может присоединиться к серверу
                                {
                                    //добавить в словарь игроков <nick, IP>
                                    Program.loggedNicks.Add(nick, handler);
                                    //если все OK послать OK
                                    Send(handler, ((char)10).ToString() + " <EOF>");
                                }

                                state.buffer = new byte[1024];
                                state.sb = new StringBuilder();
                                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReadCallback), state);
                                break;
                            }
                        case 12: //согласен играть
                            {
                                nick = parameters[1];
                                enemyNick = parameters[2];              
                                //отправить ок игроку, с которым хочешь играть
                                if (Program.enemiesoffers.ContainsKey(nick))
                                {
                                    if (nick != enemyNick) //если я не отклоняю
                                    {
                                        if (Program.enemiesoffers[nick].Contains(enemyNick))
                                        {
                                            if (Program.loggedNicks.ContainsKey(enemyNick))
                                            {
                                                Send(Program.loggedNicks[enemyNick], ((char)10).ToString() + " <EOF>");
                                                Program.loggedplayingNicks.Add(nick, Program.loggedNicks[nick]);
                                                Program.loggedplayingNicks.Add(enemyNick, Program.loggedNicks[enemyNick]);
                                                if (Program.enemiesoffers.ContainsKey(enemyNick))
                                                {
                                                    //отклонить предложения
                                                    foreach (var item in Program.enemiesoffers[enemyNick])
                                                    {
                                                        //отправить остальным фейл
                                                        if (Program.loggedNicks.ContainsKey(item))
                                                        {
                                                            Send(Program.loggedNicks[item], ((char)9).ToString() + " <EOF>");
                                                        }
                                                    }
                                                }
                                                Program.loggedNicks.Remove(nick);
                                                Program.loggedNicks.Remove(enemyNick);
                                            }
                                            else
                                            {
                                                //игрок сдался, вы победили
                                                Send(Program.loggedNicks[nick], ((char)17).ToString() + " <EOF>");
                                            }
                                            Program.enemiesoffers[nick].Remove(enemyNick);
                                        }
                                    }
                                    foreach (var item in Program.enemiesoffers[nick])
                                    {
                                        //отправить остальным фейл
                                        if (Program.loggedNicks.ContainsKey(item))
                                        {
                                            Send(Program.loggedNicks[item], ((char)9).ToString() + " <EOF>");
                                        }                                     
                                    }
                                    Program.enemiesoffers[nick].Clear(); //очистить лист
                                }
                                
                                state.buffer = new byte[1024];
                                state.sb = new StringBuilder();
                                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReadCallback), state);
                                break;
                            }

                        case 13: //GetEnemies <nick>
                            {
                                //получить ник
                                nick = parameters[1];
                                //проверить есть ли ник в словаре
                                result = Program.loggedNicks.ContainsKey(nick);
                                if (result == false) //если нет в словаре отправить фейл запросу
                                {                          
                                    Send(handler, ((char)12).ToString() + " <EOF>");
                                }
                                else
                                {
                                    //получить игроков из словаря только ники
                                    players = "";
                                    foreach (var item in Program.loggedNicks)
                                    {
                                        //пропустить человека с <nick>
                                        if (!item.Key.Equals(nick))
                                            players += item.Key + ";" + item.Value.LocalEndPoint + " ";
                                    }
                                    if (players == "") //если никто не онлайн отправить фейл запрос
                                    {
                                        Send(handler, ((char)12).ToString() + " <EOF>");
                                    }
                                    else //отправить игрокам запрос
                                    {
                                        players += "<EOF>";
                                        players = ((char)12).ToString()+" " + players;

                                        //отправить соперникам
                                        Send(handler, players);
                                    }
                                }

                                state.buffer = new byte[1024];
                                state.sb = new StringBuilder();
                                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReadCallback), state);
                                break;
                            }
                        case 14://закрыть приложение
                            {
                                whoSent = parameters[1];
                                //получить ник
                                whomSent = parameters[2];
                                //

                                //проверить в словаре ли ник
                                if (Program.loggedNicks.ContainsKey(whoSent) ==true)
                                {
                                    //Program.whowhomSentGiveUp.Add(whoSent + whomSent);
                                    Program.loggedNicks.Remove(whoSent);
                                }
                                if (Program.loggedplayingNicks.ContainsKey(whoSent) == true)
                                {
                                    Program.loggedplayingNicks.Remove(whoSent);
                                }

                                if (Program.enemiesoffers.ContainsKey(whoSent))
                                {
                                    //отклонить предложения врагов
                                    foreach (var item in Program.enemiesoffers[whoSent])
                                    {
                                        //отправить остальным фейл
                                        if (Program.loggedNicks.ContainsKey(item))
                                        {
                                            Send(Program.loggedNicks[item], ((char)9).ToString() + " <EOF>");
                                        }
                                    }
                                }

                                handler.Shutdown(SocketShutdown.Both);
                                handler.Close();
                                break;
                            }
                        case 15://сдаться в основной игре
                            {
                                nick = parameters[1];
                                enemyNick = parameters[2];

                                if (Program.loggedplayingNicks.ContainsKey(nick))
                                {
                                    if (!Program.loggedNicks.ContainsKey(nick))
                                    {
                                        Program.loggedNicks.Add(nick, Program.loggedplayingNicks[nick]);
                                        Program.loggedplayingNicks.Remove(nick);
                                    }
                                }
                                if (Program.loggedplayingNicks.ContainsKey(enemyNick))
                                {
                                    if (!Program.loggedNicks.ContainsKey(enemyNick))
                                    {
                                        Program.loggedNicks.Add(enemyNick, Program.loggedplayingNicks[enemyNick]);
                                        Program.loggedplayingNicks.Remove(enemyNick);
                                        Send(Program.loggedNicks[enemyNick], ((char)17).ToString() + " <EOF>");
                                    }
                                }
                                state.buffer = new byte[1024];
                                state.sb = new StringBuilder();
                                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReadCallback), state);
                                break;
                            }
                    }
                }

            }
            else
            {
                //не все данные получены, считать еще
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
            }
        }

        private static void Send(Socket handler, String data)
        {
            //преобразовать строку данных в байт данные используя ASCII.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            //начать отправлять данные на удаленное устройство
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                //Получить сокет из объекта состояния
                Socket handler = (Socket)ar.AsyncState;

                //завершить отправлять данных удаленному устройству
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Отправлено {0} байт клиенту.", bytesSent);

            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
            }
        }
    }
}
