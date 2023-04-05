using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;

namespace KosovPractice17
{
    /// <summary>
    /// Логика взаимодействия для AddRecord.xaml
    /// </summary>
    public partial class AddRecord : Window
    {
        public AddRecord()
        {
            InitializeComponent();
            this.Height += 25;
        }

        TeamEntities db = TeamEntities.GetContext();
        TeamMember p1 = new TeamMember();
        
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add(object sender, RoutedEventArgs e)

        {
            StringBuilder errors = new StringBuilder();

            if (FIO.Text.Length == 0)
                errors.AppendLine("Введите ФИО");
            if (Team.Text != "Динамо" && Team.Text != "Спартак")
                errors.AppendLine("Выберите Динамо или Спартак");
            if (DatePicker.Text.Length == 0)
                errors.AppendLine("Выберите дату приема");
            if (!Int32.TryParse(GoalCount.Text, out int x))
                errors.AppendLine("Введите число голов");
            if (!Int32.TryParse(PassCount.Text, out int y))
                errors.AppendLine("Введите число пасов");
            if (!Int32.TryParse(PenaltyTime.Text, out int z))
                errors.AppendLine("Введите штрафное время");
            if (!Int32.TryParse(Matches.Text, out int v))
                errors.AppendLine("Введите количество матчей");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            p1.Fio = FIO.Text;
            p1.Team = Team.Text;
            p1.JoinDate = DatePicker.SelectedDate;
            p1.GoalCount = x;
            p1.PassCount = y;
            p1.PenaltyTime = z;
            p1.Matches = v;

            try
            {
                db.TeamMembers.Add(p1);
                db.SaveChanges();
                MessageBox.Show("Сохранено");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
