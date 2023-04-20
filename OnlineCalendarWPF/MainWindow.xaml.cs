using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using OnlineCalendarWPF.DatabaseModel;

namespace OnlineCalendarWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string USERS_TABLE = "Users";
        public const string EVENTS_TABLE = "Events";
        public const string REMINDERS_TABLE = "Reminders";
        public const string CALENDARS_TABLE = "Calendars";
        public const string GROUPS_TABLE = "Groups";
        public const string JOINGROUP_TABLE = "JoinGroup";
        public const string ADDGROUPEVENT_TABLE = "AddGroupEvent";

        public CalendarModel SelectedCalendar { get; set; }

        public ObservableCollection<CalendarModel> CalendarsList { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ViewModel vm = new ViewModel();

            MongoCRUD db = new MongoCRUD("OnlineCalendar");
            //db.InsertRecord("Users", new UserModel {  Name = "Esterházy Péter", Email = "estike@gmail.com"});

            var users = db.LoadRecords<UserModel>(USERS_TABLE);
            Guid userId = users[0].Id;

            //db.InsertRecord(CALENDARS_TABLE, new CalendarModel { Name = "Family", Description="Family events", UserId = userId });
            //db.InsertRecord(CALENDARS_TABLE, new CalendarModel { Name = "Work", Description="Work related events", UserId = userId });
            //db.InsertRecord(CALENDARS_TABLE, new CalendarModel { Name = "School", Description="School stuff", UserId = userId });

            //foreach(var rec in recs)
            //{
            //    Console.WriteLine(rec.Name);
            //}

            //var rec = db.LoadRecordById<UserModel>("Users", new Guid("29f5a97e-1b8c-4ab7-873c-12e2d26a3a82"));

            var calendarsList = db.LoadRecords<CalendarModel>(CALENDARS_TABLE);
            vm.GetCalendars(calendarsList);

            var events = db.LoadRecords<EventModel>(EVENTS_TABLE);
            SetBlackOutDates(events);
        }

        private void SetBlackOutDates(List<EventModel> events)
        {
            foreach(var calendarEvent in events)
            {
                //Calendar.BlackoutDates.Add(new CalendarDateRange(calendarEvent.StartDate, calendarEvent.EndDate));
                //Calendar.Background.
            }
        }

        private void Calendar_SelectedDatesChanged(object sender,SelectionChangedEventArgs e)
        {
            EventTitleTextBox.Text = string.Empty;

            StartDateTextBox.Text = Calendar.SelectedDate.ToString();
            if (Calendar.SelectedDates.Count > 1) 
            {
                EndDateTextBox.Text = Calendar.SelectedDates[Calendar.SelectedDates.Count - 1].ToString();
            }
            else
            {
                EndDateTextBox.Text = Calendar.SelectedDate.ToString();
            }

            // todo megnézni, hogy ez létező event-e
            DateTime startDate = DateTime.Parse(StartDateTextBox.Text, new CultureInfo("hu-HU"));
            DateTime endDate = DateTime.Parse(EndDateTextBox.Text, new CultureInfo("hu-HU"));
            LoadEventIfExists(startDate, endDate);
        }

        public void LoadEventIfExists(DateTime startDate, DateTime endDate)
        {
            MongoCRUD db = new MongoCRUD("OnlineCalendar");

            var events = db.LoadRecords<EventModel>(EVENTS_TABLE);
            foreach(var eventItem in events)
            {
                if (eventItem.StartDate == startDate && eventItem.EndDate == endDate)
                {
                    EventTitleTextBox.Text = eventItem.Title;
                }
            }
        }

        private void Calendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e) { }
        private void Calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e) { }

        private void SaveEventButton_Click(object sender, RoutedEventArgs e)
        {
            MongoCRUD db = new MongoCRUD("OnlineCalendar");

            DateTime startDate = DateTime.Parse(StartDateTextBox.Text, new CultureInfo("hu-HU"));
            DateTime endDate = DateTime.Parse(EndDateTextBox.Text, new CultureInfo("hu-HU"));
            
            // todo itt csekkolni hogy volt-e már ilyen event (update vagy create)

            db.InsertRecord(EVENTS_TABLE, new EventModel { Title=EventTitleTextBox.Text, StartDate=startDate, EndDate=endDate, CreatedAt=DateTime.Now, UpdatedAt=DateTime.Now });
        }

        private void StartDateTextBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                DateTime startDate = DateTime.Parse(StartDateTextBox.Text, new CultureInfo("hu-HU"));
                DateTime endDate = DateTime.Parse(EndDateTextBox.Text, new CultureInfo("hu-HU"));
                LoadEventIfExists(startDate, endDate);
            }
            catch
            { // 
            }
            
        }
    }


    public class ViewModel
    {
        public CalendarModel SelectedCalendar { get; set; }

        public ObservableCollection<CalendarModel> CalendarsList { get; set; }

        public DateTime SelectedStartDate { get; set; }
        public DateTime SelectedEndDate { get; set; }

        public void GetCalendars(List<CalendarModel> calendarsList)
        {
            CalendarsList = new ObservableCollection<CalendarModel>();

            foreach (var calendarItem in calendarsList)
            {
                CalendarsList.Add(new CalendarModel { Name = calendarItem.Name, Description=calendarItem.Description, Id=calendarItem.Id, UserId=calendarItem.UserId });
            }
        }

        //public string Valami { get { return "valami"; } }
    }

}
