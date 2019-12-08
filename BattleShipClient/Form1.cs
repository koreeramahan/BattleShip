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
    public partial class Form1 : Form
    {
        string enemyNick;
        //public bool enemyGiveUpBeforeStart = false;
        public bool normalEnd = false;
        public Button clickedButton;

        //палуба не потонула
        public int masts = 20;
        public Form1(string enemyNick)
        {
            InitializeComponent();
            DoubleBuffered = true;
            this.enemyNick = enemyNick;
        }

        public bool[,] yourMap = new bool[10, 10];
        public bool[,] yourMapTmp = new bool[10, 10];
        public bool[,] enemyMap = new bool[10, 10];
        List<Button> selectedButtons = new List<Button>();
        //параметры игрового поля
        private void setDefaultValuesInMap(bool value, bool [,] Map)
        {
            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {
                    Map[i, j] = value;
                }
            }
        }
        //организация игровых полей из кнопок
        private void GenerateMap(string name, int xStartPos, int yStartPos)
        {
            Panel buttonPanel = new Panel();
            buttonPanel.Name = name;
            buttonPanel.Size=new Size(231, 231);
            int xButtonSize = 21;
            int yButtonSize = 21;
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i == 0 && j == 0) continue; //пустой верхний левый угол
                    Button button = new Button();
                    buttonPanel.Controls.Add(button);
                    button.Location = new System.Drawing.Point(j* xButtonSize, i * yButtonSize);
                    button.Size = new Size(xButtonSize, yButtonSize);
                    button.ForeColor = System.Drawing.Color.Black;
                    //this.Controls.Add(button);
                    if (i>0 && j>0) // активная часть поля (двойные координаты 00, 01, ..., 99)
                    {
                        button.Name = (i - 1).ToString() + (j - 1).ToString();
                        //button.Text = (i - 1).ToString() + (j - 1).ToString();
                        button.Font = new Font(button.Font.FontFamily, 6);//шрифт в кнопке
                        if (name == "PYou")//если взаимодействие со своим полем
                        {
                            button.Click += new System.EventHandler(this.setMastbuttonClick);
                        }
                        else if (name== "PEnemy")//если с полем противника
                        {
                            button.Click += new System.EventHandler(this.buttonClick);
                        }
                    }
                    else //не активная часть поля, организация координат
                    {
                        button.Enabled = false;
                        if (i == 0 && j > 0) //буквы: A, Б, ..., Й
                        {
                            button.Text = ((char)(1039 + j)).ToString();
                            button.Name= ((char)(1039 + j)).ToString();
                            button.Font = new Font(button.Font.FontFamily, 6);
                        }
                        else if (i!=0 || j!=0) // цифры: 1, 2, ..., 10
                        {
                            button.Text = i.ToString();
                            button.Name= "L" + i.ToString();
                            button.Font = new Font(button.Font.FontFamily, 6);
                        }
                    }          
                }
            }
            this.Controls.Add(buttonPanel);
            buttonPanel.Location = new Point(xStartPos, yStartPos);
        }

        private void SetShips() //установка кораблей
        {
            GenerateMap("PYou",204, 190);
            setDefaultValuesInMap(false, yourMap);
            for (int i = 0; i < yourMapTmp.GetLength(1); i++)
            {
                for (int j = 0; j < yourMapTmp.GetLength(0); j++)
                {
                    yourMapTmp[i, j] = false;
                }
            }
        }
        //активность ячейки
        private void DisableOrEnableButtonIfExists(Panel panel, int x1, int y1, bool DisOrEn)
        {
            Button button;
            button = (Button)panel.Controls.Find(x1.ToString() + y1.ToString(), true).FirstOrDefault();
            if (button != null)
            {
                if (DisOrEn == false)
                {
                    button.Enabled = false;
                }
                else
                {
                    button.Enabled = true;
                }
            }
        }
        //проверка наличия соседей слева, справа, сверху, снизу 
        private int IsLeftNeighbour(int x, int y)
        {
            int x1=x;
            int y1=y-1;
            if (y1>-1)//если предполагаемый сосед не выходит за пределы поля
            {
                if (yourMap[x1,y1]==true) //предпологаемый сосед (ячейка) активна (выделена)
                {
                    return 1 + IsLeftNeighbour(x1, y1);
                }
                else //нет соседа
                {
                    return 0;
                }
            }
            else //нет соседа
            {
                return 0;
            }
        }

        private int IsRightNeighbour(int x, int y)
        {
            int x1 = x;
            int y1 = y + 1;
            if (y1 < 10)//если предполагаемый сосед не выходит за пределы поля
            {
                if (yourMap[x1, y1] == true) //предпологаемый сосед (ячейка) активна (выделена)
                {
                    return 1 + IsRightNeighbour(x1, y1);
                }
                else //нет соседа
                {
                    return 0;
                }
            }
            else // нет соседа
            {
                return 0;
            }
        }
        private int IsUpNeighbour(int x, int y)
        {
            int x1 = x - 1;
            int y1 = y;
            if (x1 > -1)// если предполагаемый сосед не выходит за пределы поля
            {
                if (yourMap[x1, y1] == true) //предпологаемый сосед (ячейка) активна (выделена)
                {
                    return 1 + IsUpNeighbour(x1, y1);
                }
                else //нет соседа
                {
                    return 0;
                }
            }
            else //нет соседа
            {
                return 0;
            }
        }
        private int IsDownNeighbour(int x, int y)
        {
            int x1 = x + 1;
            int y1 = y;
            if (x1 < 10) // если предполагаемый сосед не выходит за пределы поля
            {
                if (yourMap[x1, y1] == true) //предпологаемый сосед (ячейка) активна (выделена)
                {
                    return 1 + IsDownNeighbour(x1, y1);
                }
                else //нет соседа
                {
                    return 0;
                }
            }
            else //нет соседа
            {
                return 0;
            }
        }
        //включить или выключить углы
        private void DisableOrEnableAllCorners(Panel panel, int x, int y, bool trueOrFalse)
        {
            DisableOrEnableButtonIfExists(panel, x-1, y-1, trueOrFalse);
            DisableOrEnableButtonIfExists(panel, x-1, y+1, trueOrFalse);
            DisableOrEnableButtonIfExists(panel, x+1, y+1, trueOrFalse);
            DisableOrEnableButtonIfExists(panel, x+1, y-1, trueOrFalse);
        }
        //проверка 1палубных кораблей
        bool Check1Masts()
        {
            int leftNo = 0;
            int rightNo = 0;
            int upNo = 0;
            int downNo = 0;

            int counter = 0; // счетчик
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (yourMap[i, j] == true) // если ячейка активна(выбрана)
                    {
                        //проверка наличия соседей
                        leftNo = IsLeftNeighbour(i, j);
                        rightNo = IsRightNeighbour(i, j);
                        upNo = IsUpNeighbour(i, j);
                        downNo = IsDownNeighbour(i, j);
                        if (leftNo==0 && rightNo==0 && downNo ==0 && upNo==0)//если нет соседей
                        {
                            yourMapTmp[i, j] = true;//добавить ячейку в словарь
                            counter++;
                        }
                        if (counter > 4) return false;//если расставлено больше 4х доступных кораблей, выдает ошибку
                    }

                }
            }
            if (counter < 4) return false;// если расставлено меньше 4х доступных кораблей, выдает ошибку
            return true;
        }
        //проверка 2палубных кораблей
        bool Check2Masts()
        {
            int leftNo = 0;
            int rightNo = 0;
            int upNo = 0;
            int downNo = 0;

            int counter = 0;//счетчик
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (yourMap[i, j] == true)//если ячейка активна
                    {
                        if (yourMapTmp[i, j] == true) continue;//если ячейка уже есть в словаре
                        //проверка соседей по вертикали
                        downNo = IsDownNeighbour(i, j);
                        upNo = IsUpNeighbour(i, j);
                        if (downNo == 1 && upNo==0)//если есть сосед снизу и нет сверху
                        {
                            //добавить текущую ячейку и соседа снизу в словарь
                            yourMapTmp[i, j] = true;
                            yourMapTmp[i + 1, j] = true;
                            counter++;
                        }
                        else if (downNo==0 && upNo==0)//если соседей по вертикали нет
                        {
                            //проверка соседей по горизонтали
                            rightNo = IsRightNeighbour(i, j);
                            leftNo = IsLeftNeighbour(i, j);
                            if (rightNo==1 && leftNo==0)//если есть сосед справа и нет слева
                            {
                                //добавить текущую ячейку и яцейку слева в словарь
                                yourMapTmp[i, j] = true;
                                yourMapTmp[i, j+1] = true;
                                counter++;
                            }
                        }
                        if (counter > 3) return false;//если расставлено больше 3х доступных кораблей, выдает ошибку
                    }
                }
            }
            if (counter < 3) return false;//если расставлено меньше 3х доступных кораблей, выдает ошибку
            return true;
        }

        //проверка 3палубных кораблей
        bool Check3Masts()
        {
            int leftNo = 0;
            int rightNo = 0;
            int upNo = 0;
            int downNo = 0;

            int counter = 0;//счетчик
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (yourMap[i, j] == true)//если ячейка активна
                    {
                        if (yourMapTmp[i, j] == true) continue;//если ячейка уже есть в словаре
                        //проверка соседей по вертикали
                        downNo = IsDownNeighbour(i, j);
                        upNo = IsUpNeighbour(i, j);

                        if (downNo == 2 && upNo == 0)//если снизу есть 2 соседа
                        {
                            //занести всех в словарь
                            yourMapTmp[i, j] = true;
                            yourMapTmp[i + 1, j] = true;
                            yourMapTmp[i + 2, j] = true;
                            counter++;
                        }
                        else if (downNo == 0 && upNo == 0)
                        {
                            //проверка соседей по горизонтали
                            rightNo = IsRightNeighbour(i, j);
                            leftNo = IsLeftNeighbour(i, j);
                            if (rightNo == 2 && leftNo == 0)//если справа есть 2 соседа
                            {
                                //занести всех в словарь
                                yourMapTmp[i, j] = true;
                                yourMapTmp[i, j + 1] = true;
                                yourMapTmp[i, j + 2] = true;
                                counter++;
                            }
                        }
                        if (counter > 2) return false;//если расставлено больше 2х доступных кораблей, выдает ошибку
                    }
                }
            }
            if (counter < 2) return false;//если расставлено меньше 2х доступных кораблей, выдает ошибку
            return true;
        }
        //проверка 4палубных кораблей
        bool Check4Masts()
        {
            int leftNo = 0;
            int rightNo = 0;
            int upNo = 0;
            int downNo = 0;

            int counter = 0;//счетчик
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (yourMap[i, j] == true)//если ячейка активна
                    {
                        if (yourMapTmp[i, j] == true) continue;//если ячейка уже есть в словаре
                        //проверка соседей по вертикали
                        downNo = IsDownNeighbour(i, j);
                        upNo = IsUpNeighbour(i, j);
                        if (downNo == 3 && upNo == 0)//если снизу есть 3 соседа
                        {
                            //добавить всех в словарь
                            yourMapTmp[i, j] = true;
                            yourMapTmp[i + 1, j] = true;
                            yourMapTmp[i + 2, j] = true;
                            yourMapTmp[i + 3, j] = true;
                            counter++;
                        }
                        else if (downNo == 0 && upNo == 0)
                        {
                            //проверка соседей по горизонтали
                            rightNo = IsRightNeighbour(i, j);
                            leftNo = IsLeftNeighbour(i, j);
                            if (rightNo == 3 && leftNo == 0)//если есть 3 справа
                            {
                                //добавить всех в словарь
                                yourMapTmp[i, j] = true;
                                yourMapTmp[i, j + 1] = true;
                                yourMapTmp[i, j + 2] = true;
                                yourMapTmp[i, j + 3] = true;
                                counter++;
                            }
                        }
                        if (counter > 1) return false;//если расставлено больше 1 доступного корабля, выдает ошибку
                    }
                }
            }
            if (counter < 1) return false;//если расставлено больше 1 доступного кораблея, выдает ошибку
            return true;
        }       
        //подготовка к бою
        public void PrepareBattleField()
        {         
            //спрятать панель для установки палуб
            PMastSet.Visible = false;
            Panel matched = (Panel)this.Controls.Find("PYou", true).FirstOrDefault();
            matched.Visible = false;
            matched.Enabled = false;
            if (matched != null)
            {
                matched.Location = new Point(matched.Location.X - 164, matched.Location.Y);
            }
            matched.Visible = true;
            //поле врага
            GenerateMap("PEnemy", 360, 190);
            //сделать видимым перечень кораблей
            PMast.Visible = true;
            Array.Clear(yourMapTmp, 0, yourMapTmp.Length);//очистить словарь
        }
        //нажатие кнопки играть
        void playbuttonClick(object sender, EventArgs e)
        {
            clickedButton = (Button)sender;
            //проверить палубы
            bool checkResult = false;
            Array.Clear(yourMapTmp, 0, yourMapTmp.Length);
            //проверка количества расставленных кораблей
            checkResult=Check1Masts();
            if (checkResult==false)
            {
                MessageBox.Show("Неправильное количество 1-палубных кораблей", "Ошибка");
                return;
            }
            checkResult = Check2Masts();
            if (checkResult == false)
            {
                MessageBox.Show("Неправильное количество 2-палубных кораблей", "Ошибка");
                return;
            }
            checkResult = Check3Masts();
            if (checkResult == false)
            {
                MessageBox.Show("Неправильное количество 3-палубных кораблей", "Ошибка");
                return;
            }
            checkResult = Check4Masts();
            if (checkResult == false)
            {
                MessageBox.Show("Неправильное количество 4-палубных кораблей", "Ошибка");
                return;
            }

            //отправить StartGame запрос
            char comm = (char)0;
            string message = comm + " " + Program.userLogin + " " + Program.enemyNick + " <EOF>";
            Program.client.Send(message);
            //enemyGiveUpBeforeStart = true;
            //получить ответ в Program, деактивировать кнопку "играть"
            clickedButton.Enabled = false;
            ExitButton.Enabled = true;
        }
        //обработка выстрела
        public void GetShotAndResponse(int x, int y)
        {
            Button button;
            Panel panel = (Panel)this.Controls.Find("PYou", true).FirstOrDefault();
            button = (Button)panel.Controls.Find(x.ToString() + y.ToString(), true).FirstOrDefault();
            string message = "";           
            //в корабль попали
            if (yourMap[x,y] == true)//если ячейка активна
            {
                yourMapTmp[x, y] = true;//запись ячейки в словарь
                masts--;//отсчет оставшихся мачт      
                //изменить цвет мачты на карте
                button.BackColor = Color.Tomato;
                Application.DoEvents();
                if (masts == 0)//если непотопленных мачт не осталось
                {
                    Application.DoEvents();
                    return;
                }
                else//если мачты еще есть
                {
                    //отправить попадание
                    message = (char)5 + " " + enemyNick + " <EOF>";
                    Program.client.Send(message);
                    //продолжить ход
                    ((Panel)this.Controls.Find("PEnemy", true).FirstOrDefault()).Enabled = false;
                }  
            }
            else//не попали
            {
                //отправить промах
                message = (char)4 + " " + enemyNick + " <EOF>";
                Program.client.Send(message);
                button.BackColor = Color.Silver;
                //передать ход
                ((Panel)this.Controls.Find("PEnemy", true).FirstOrDefault()).Enabled = true;
            }
            Application.DoEvents();
        }

        //расстановка кораблей
        void setMastbuttonClick(object sender, EventArgs e)
        {
            var clickedButton = (Button)sender;//отследить какая кнопка нажата
            int x = Int32.Parse(clickedButton.Name.Substring(0, 1)); //получить x координаты
            int y = Int32.Parse(clickedButton.Name.Substring(1, 1)); //получить y координаты
            int leftNo = IsLeftNeighbour(x, y);
            int rightNo = IsRightNeighbour(x, y);
            int upNo = IsUpNeighbour(x, y);
            int downNo = IsDownNeighbour(x, y);

            //выбрана или нет
            if (clickedButton.BackColor != Color.MediumBlue) //если ячейка не выбрана
            {
                //проверить соседей по периметру
                if ((leftNo + rightNo < 4) && (upNo + downNo < 4))
                {
                    //закрасить
                    clickedButton.BackColor = Color.MediumBlue;
                    //добавить в лист
                    selectedButtons.Add(clickedButton);
                    //отключить нажатие углов
                    DisableOrEnableAllCorners((Panel)clickedButton.Parent, x, y, false);
                    //добавить на игровое поле
                    yourMap[x, y] = true;
                }
            }
            else//если была выбрана
            {
                //обесцветить
                clickedButton.BackColor = Color.Transparent;
                //сделать углы доступными
                DisableOrEnableAllCorners((Panel)clickedButton.Parent, x, y, true);
                //удалить из листа
                selectedButtons.Remove(clickedButton);
                //отключить нажатие на углы в листе кнопок, для повторяющихся
                foreach (Button btn in selectedButtons)
                {
                    DisableOrEnableAllCorners((Panel)btn.Parent, Int32.Parse(btn.Name[0].ToString()), Int32.Parse(btn.Name[1].ToString()), false);
                }
                //убрать с игрового поля
                yourMap[x, y] = false;
            }
        }
        //выстрел
        void buttonClick(object sender, EventArgs e)
        {
            clickedButton = (Button)sender;//отследить какая кнопка нажата
            clickedButton.Enabled = false;
            int x = Int32.Parse(clickedButton.Name.Substring(0, 1)); //получить x координаты
            int y = Int32.Parse(clickedButton.Name.Substring(1, 1)); //получить y координаты
            //отправить Выстрелил
            string message = "";
            message = (char)6 + " " + enemyNick + " " + x.ToString() +" " + y.ToString() + " <EOF>";
            Program.client.Send(message);
            //получить ответ       
        }
        //загрузка игрового окна
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Морской бой - вы играете с " + enemyNick;
            SetShips();
            
        }
        ////закрытие окна по нажатию крестика
        //private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (enemyGiveUpBeforeStart == false)//сдался до начала игры
        //    {
        //        //пользователь должен отправить Сдаюсь запрос
        //        char comm = (char)2;
        //        //<кто сдается> <с кем играет>
        //        string message = comm + " " + Program.userLogin + " " + Program.enemyNick + " <EOF>";
        //        Program.client.Send(message);
        //        DialogResult = DialogResult.Yes;
        //    }
        //    else if (normalEnd == false) //закрыл до окнчания игры
        //    {
        //        char comm = (char)15;
        //        //<кто сдается> <с кем играет>
        //        string message = comm + " " + Program.userLogin + " " + Program.enemyNick + " <EOF>";
        //        Program.client.Send(message);
        //        DialogResult = DialogResult.Yes;
        //    }
        //    //пользователь возвращается к таблице выбора игрока
        //}

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
   System.Windows.Forms.DialogResult.Yes)
            {
                //if (enemyGiveUpBeforeStart == false)
                //{
                //    //пользователь должен отправить Сдаюсь запрос
                //    char comm = (char)2;
                //    //<кто сдается> <с кем играет>
                //    string message = comm + " " + Program.userLogin + " " + Program.enemyNick + " <EOF>";
                //    Program.client.Send(message);
                //    DialogResult = DialogResult.Yes;
                //}
                if (normalEnd == false)
                {
                    char comm = (char)15;
                    //<кто сдается> <с кем играет>
                    string message = comm + " " + Program.userLogin + " " + Program.enemyNick + " <EOF>";
                    Program.client.Send(message);
                    DialogResult = DialogResult.Yes;
                }
            }
        }
    }
}
