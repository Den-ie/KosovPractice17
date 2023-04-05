using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static KosovPractice17.EditRecord;

namespace KosovPractice17
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Height += 25;
        }

        TeamEntities db = TeamEntities.GetContext();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.TeamMembers.Load();
            DB.ItemsSource = db.TeamMembers.Local.ToBindingList();
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            AddRecord f = new AddRecord();
            f.ShowDialog();
            DB.Focus();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            int indexRow = DB.SelectedIndex;
            if (indexRow != -1)
            {
                TeamMember row = (TeamMember)DB.Items[indexRow];
                Data.Id = row.id;
                EditRecord f = new EditRecord();
                f.ShowDialog();
                DB.Items.Refresh();
                DB.Focus();
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    TeamMember row = (TeamMember)DB.SelectedItems[0];
                    db.TeamMembers.Remove(row);
                    db.SaveChanges();
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Выбирите запись");
                }
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Косов Даниил ИСП-34 \nВариант №4 \r\nСведения о нападающих команд “Спартак” и “Динамо”. \r\nБаза данных должна содержать следующую информацию: фамилию, имя, отчество, название команды, дату приема в \r\nкоманду, число заброшенных шайб, количество голевых передач, штрафное время и количество сыгранных матчей.");
        }

        private void FindButton(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < DB.Items.Count; i++)
            {
                var row = (TeamMember)DB.Items[i];
                string findContent = row.Fio;
                try
                {
                    if (findContent != null && findContent.Contains(txtFind.Text))
                    {
                        object item = DB.Items[i];
                        DB.SelectedItem = item;
                        DB.ScrollIntoView(item);
                        DB.Focus();
                        break;
                    }
                }
                catch { }
            }
        }

        List<TeamMember> _teammember;

        private void FilterButton(object sender, RoutedEventArgs e)
        {
            _teammember = db.TeamMembers.ToList();
            var filtered = _teammember.Where(_teammember => _teammember.Fio.Contains(txtFilter.Text));
            DB.ItemsSource = filtered;
        }
    }
}
