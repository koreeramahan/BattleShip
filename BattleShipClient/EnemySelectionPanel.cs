using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Threading;

namespace BattleShipClient
{
    public partial class EnemySelectionPanel : Form
    {
        public volatile System.Windows.Forms.Timer updateTimer;
        public string enemyNick = "";
        string enemyAddressIPAndPort = "";
        public Button agreeButton;
        public List<string> onlineEnemyList = new List<string>();
        //панель игрока
        public EnemySelectionPanel()
        {         
            InitializeComponent();
            this.Text = "Морской бой - " + Program.userLogin;                  
        }
        //организация таймера
        private void SetTimer()
        {
            //Создать таймер с 15 секундным интервалом
            updateTimer = new System.Windows.Forms.Timer();// (15000);//61000
            // Течение времени
            updateTimer.Interval = 5000;
            updateTimer.Tick += new EventHandler(OnEnemyTimedEvent);
            //pdateTimer.Elapsed += OnEnemyTimedEvent;
            //updateTimer.AutoReset = true;
            updateTimer.Enabled = true;
            GetEnemies();
        }
        //обновление списка доступных игроков
        private void OnEnemyTimedEvent(Object source, EventArgs e)
        {
            GetEnemies();
            Thread.Sleep(200);
            updateTimer.Enabled = false;
            GetOffers();
        }
        //перечень соперников
        public void GetEnemies()
        {
            //отправить GetEnemies запрос
            char comm = (char)13;
            string message = comm + " " + Program.userLogin + " <EOF>"; //соперники кроме меня
            Program.client.Send(message);
            //получить ответ         
        }
        //перечень игроков
        public void GetOffers()
        {
            //Отправить Getoffers запрос
            char comm = (char)7;
            string message = comm + " " + Program.userLogin + " <EOF>"; //соперники кроме меня
            Program.client.Send(message);
            //получить ответ         
        }
        //нажатее кнопки подключиться
        private void BConnect_Click(object sender, EventArgs e)
        {
            if (enemyNick != "")
            {
                updateTimer.Enabled = false;
                //Отправить Offer запрос
                string message = (char)8 + " " + Program.userLogin + " " + enemyNick +" <EOF>"; //соперники кроме меня
                Program.client.Send(message);
                //Получить ответ       
                agreeButton = (Button)sender;
                agreeButton.Enabled = false;
            }
        }
        //закрыть приложение
        private void CloseApp(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                DialogResult = DialogResult.No;
                //Program.dialog = 0;
                //Send CloseApp communique
                char comm = (char)14;
                string message = comm + " " + Program.userLogin + " "+ enemyNick+ " <EOF>";
                Program.client.Send(message);              
            }
            if (updateTimer.Enabled == true)
            {
                updateTimer.Enabled = false;
            }
        }
        //впоиск противника по нику
        private void SearchEnemy(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DGVAvailableEnemies.Rows) //вы получаете сообщение
            {
                row.Visible = row.Cells[1].Value.ToString().ToLower().StartsWith(TBSearch.Text.ToLower());//получатель
            }
        }
        //панель выбора противник
        private void EnemySelectionPanel_Load(object sender, EventArgs e)
        {
            DGVAvailableEnemies.ClearSelection();
            SetTimer();
        }
        //нажатие кнопки выход
        private void ExitButton_Click(object sender, EventArgs e)
        {
            //реализация выхода из игры 
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
            System.Windows.Forms.DialogResult.Yes)
                Application.Exit();
        }
        //выбор игрока из списка
        private void DGVAvailableEnemies_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int row = e.RowIndex;
                //получить инфу о нажатом игроке
                enemyAddressIPAndPort = DGVAvailableEnemies.Rows[row].Cells[0].Value.ToString();
                enemyNick = DGVAvailableEnemies.Rows[row].Cells[1].Value.ToString();
                BConnect.Text = "Играть с " + enemyNick;
            }
        }
    }
}
