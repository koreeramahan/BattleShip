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
    public partial class OfferingGame : Form
    {
        string enemiesNicks;
        public OfferingGame(string enemiesNicks)
        {
            InitializeComponent();
            DialogResult = DialogResult.No;
            this.enemiesNicks = enemiesNicks;
        }
        //нажата кнопка Начать игру
        private void BYes_Click(object sender, EventArgs e)
        {
            if (CBEneNicks.Text == "")//если не выбран соперник
            {
                MessageBox.Show("Вам нужно выбрать соперника или нажать Отклонить", "Ошибка!");
            }
            else
            {
                //получаем айди выбранного соперника
                Program.enemySelect.enemyNick = CBEneNicks.SelectedValue.ToString();
                Program.enemyNick = CBEneNicks.SelectedValue.ToString();
                DialogResult = DialogResult.Yes;
            }
        }
        //Отклонение игры
        private void BNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void OfferingGame_Load(object sender, EventArgs e)
        {
            List<string> eNicks = enemiesNicks.Split(' ').ToList();
            eNicks.RemoveAt(eNicks.Count()-1); //EOF
            eNicks.RemoveAt(0);

            CBEneNicks.DataSource = eNicks;
            CBEneNicks.AutoCompleteMode = AutoCompleteMode.Append;
            CBEneNicks.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
    }
}
