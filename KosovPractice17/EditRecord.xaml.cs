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

namespace KosovPractice17
{
    /// <summary>
    /// Логика взаимодействия для EditRecord.xaml
    /// </summary>
    public partial class EditRecord : Window
    {
        public EditRecord()
        {
            InitializeComponent();
            this.Height += 25;
        }

        public static class Data { public static int Id; }

        TeamEntities db = TeamEntities.GetContext();
        TeamMember p1 = new TeamMember();


        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            p1 = db.TeamMembers.Find(Data.Id);
            FIO.Text = p1.Fio;
            Team.Text = p1.Team;
            DatePicker.SelectedDate = p1.JoinDate;
            GoalCount.Text = p1.GoalCount.ToString();
            PassCount.Text = p1.PassCount.ToString();
            PenaltyTime.Text = p1.PenaltyTime.ToString();
            Matches.Text = p1.Matches.ToString();
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
                db.SaveChanges();
                MessageBox.Show("Сохранено");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

    }
}
