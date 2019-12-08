using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShipClient
{
    public partial class ServerConnectionPanel : Form
    {
        public ServerConnectionPanel()
        {
            InitializeComponent();
        }

        private void BConnect_Click(object sender, EventArgs e)
        {
            //одно из полей не заполнено
            if (TBNick.Text == "" || TBServerIP.Text == "")
            {
                MessageBox.Show("Заполните все поля!", "Ошибка!");
            }
            //имя не в правильном формате
            else if (TBNick.Text.Split(' ').Count()>1)
            {
                MessageBox.Show("Ваше имя не должно содержать пробелы!", "Ошибка!");
            }
            else
            {
                try
                {
                    //установка логина
                    Program.userLogin = TBNick.Text;
                    Program.client = new SynchronousSocketClient(TBServerIP.Text);
                    //Отправить сообщение, чтобы присоединиться к игре
                    char comm = (char)11;                
                    string message = comm+ " " + TBNick.Text + " <EOF>";
                    Program.client.Send(message);
                    //Получить ответ
                    var answer = Program.client.Receive()[0];
                    
                    //если ответ - true
                    if (answer == (char)10)
                    {
                        DialogResult = DialogResult.Yes;
                    }
                    else//если ответ false - char(9)
                    {
                        MessageBox.Show("Введенный ник уже занят! Попробуйте другой!", "Ошибка!");
                    }                 
                }
                catch (Exception)//не удалось подключиться к серверу
                {
                    MessageBox.Show("Проблема подключения к серверу или неверный IP адрес!", "Ошибка!");
                }
            }
            
        }
        //нажатие Enter
        private void EnterClicked(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                BConnect_Click(sender, e);
            }
        }

        private void RulesButton_Click(object sender, EventArgs e)
        {
            //инструкция 
            MessageBox.Show("Добро пожаловать в игру <Морской бой>\n" +
            "Придумайте себе имя и введите IP сервера, чтобы присоединиться.\n" +
            "При успешном соединении вам станет доступен список игроков, с которыми вы можете сыграть.\n" +
            "Выберите игрока и предложите ему сыграть, либо примите чей-нибудь запрос.\n" +
            "Расставьте свои корабли на поле и нажмите кнопку готовности. По вашей готовности и готовности вашего соперника начнется игра.\n" +
            "Игра будет длиться, пока кто-то из вас не уничтожит все корабли противника.\n" + 
            "Если вы хотите сдаться, нажмите на кнопку <Вернуться в главное меню>.", "Как играть",
            MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            //реализация выхода из игры 
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
            System.Windows.Forms.DialogResult.Yes)
                Application.Exit();
        }
    }
}
