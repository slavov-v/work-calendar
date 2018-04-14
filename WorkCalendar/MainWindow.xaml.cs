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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkCalendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Task> currentTasks;
        private List<Meeting> currentMeetings;
        
        public MainWindow()
        {
            InitializeComponent();
            triggerGeneralMode();
            populateLists();
        }

        private DateTime GetDefaultDate()
        {
            return mainViewCalendar.SelectedDate.HasValue ? mainViewCalendar.SelectedDate.Value : DateTime.Now;
        }

        private void populateLists()
        {
            listBox_Meetings.Items.Clear();
            listBox_Tasks.Items.Clear();

            currentTasks = EventManager.GetTasksDueDate(GetDefaultDate());
            currentMeetings = EventManager.GetMeetingsOnDate(GetDefaultDate());

            foreach (Task item in currentTasks)
            {
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = item.Description;
                listBox_Tasks.Items.Add(listBoxItem);
            }

            foreach (Meeting item in currentMeetings)
            {
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = item.Description;
                listBox_Meetings.Items.Add(listBoxItem);
            }
        }

        private void triggerAddTaskMode()
        {
            btn_AddTask.Visibility = Visibility.Hidden;
            btn_AddMeeting.Visibility = Visibility.Hidden;
            listBox_Tasks.Visibility = Visibility.Hidden;
            listBox_Meetings.Visibility = Visibility.Hidden;
            btn_ConfirmAddMeeting.Visibility = Visibility.Hidden;
            btn_RemoveMeeting.Visibility = Visibility.Hidden;
            btn_RemoveTask.Visibility = Visibility.Hidden;
            btn_ConfirmAddTask.Visibility = Visibility.Visible;
            tb_Description.Visibility = Visibility.Visible;
            btn_Cancel.Visibility = Visibility.Visible;
            lb_Meetings.Content = "Task Description:";
            lb_Tasks.Content = string.Empty;
        }

        private void triggerAddMeetingMode()
        {
            btn_AddTask.Visibility = Visibility.Hidden;
            btn_AddMeeting.Visibility = Visibility.Hidden;
            listBox_Tasks.Visibility = Visibility.Hidden;
            listBox_Meetings.Visibility = Visibility.Hidden;
            btn_ConfirmAddTask.Visibility = Visibility.Hidden;
            btn_RemoveMeeting.Visibility = Visibility.Hidden;
            btn_RemoveTask.Visibility = Visibility.Hidden;
            btn_ConfirmAddMeeting.Visibility = Visibility.Visible;
            tb_Description.Visibility = Visibility.Visible;
            btn_Cancel.Visibility = Visibility.Visible;
            lb_Meetings.Content = "Meeting Description:";
            lb_Tasks.Content = string.Empty;
        }

        private void triggerGeneralMode()
        {
            btn_ConfirmAddTask.Visibility = Visibility.Hidden;
            tb_Description.Visibility = Visibility.Hidden;
            btn_ConfirmAddMeeting.Visibility = Visibility.Hidden;
            btn_Cancel.Visibility = Visibility.Hidden;
            btn_AddTask.Visibility = Visibility.Visible;
            btn_AddMeeting.Visibility = Visibility.Visible;
            listBox_Tasks.Visibility = Visibility.Visible;
            listBox_Meetings.Visibility = Visibility.Visible;
            btn_RemoveMeeting.Visibility = Visibility.Visible;
            btn_RemoveTask.Visibility = Visibility.Visible;
            lb_Tasks.Content = string.Format("Tasks due {0}", GetDefaultDate().Date.ToString("dd/MM/yyyy"));
            lb_Meetings.Content = string.Format("Meetings on {0}", GetDefaultDate().Date.ToString("dd/MM/yyyy"));
            tb_Description.Text = string.Empty;
        }

        private void mainViewCalendar_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            populateLists();
            lb_Tasks.Content = string.Format("Tasks due {0}", GetDefaultDate().Date.ToString("dd/MM/yyyy"));
            lb_Meetings.Content = string.Format("Meetings on {0}", GetDefaultDate().Date.ToString("dd/MM/yyyy"));
        }

        private void btn_AddTask_Click(object sender, RoutedEventArgs e)
        {
            triggerAddTaskMode();
        }

        private void btn_ConfirmAddTask_Click(object sender, RoutedEventArgs e)
        {
            if(tb_Description.Text.Length == 0)
            {
                MessageBox.Show("Description can not be empty!");
                return;
            }

            Task newTask = new Task(tb_Description.Text, DateTime.Now, GetDefaultDate());
            newTask.Save();
            triggerGeneralMode();
            populateLists();
        }

        private void btn_AddMeeting_Click(object sender, RoutedEventArgs e)
        {
            triggerAddMeetingMode();
        }

        private void btn_ConfirmAddMeeting_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Description.Text.Length == 0)
            {
                MessageBox.Show("Description can not be empty!");
                return;
            }

            
            Meeting newMeeting = new Meeting(tb_Description.Text, GetDefaultDate());
            newMeeting.Save();
            triggerGeneralMode();
            populateLists();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            triggerGeneralMode();
        }

        private void btn_RemoveMeeting_Click(object sender, RoutedEventArgs e)
        {
            if(listBox_Meetings.SelectedIndex < 0)
            {
                MessageBox.Show("No Meeting has been selected!");
                return;
            }

            EventManager.RemoveMeeting(currentMeetings[listBox_Meetings.SelectedIndex].Id);
            populateLists();

        }

        private void btn_RemoveTask_Click(object sender, RoutedEventArgs e)
        {
            if(listBox_Tasks.SelectedIndex < 0)
            {
                MessageBox.Show("No Task has been selected!");
                return;
            }

            EventManager.RemoveTask(currentTasks[listBox_Tasks.SelectedIndex].Id);
            populateLists();
        }
    }
}
