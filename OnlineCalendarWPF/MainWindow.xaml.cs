using System.Windows;
using System.Windows.Ink;
using OnlineCalendarWPF.DatabaseModel;

namespace OnlineCalendarWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MongoCRUD db = new MongoCRUD("OnlineCalendar");
            //db.InsertRecord("Users", new UserModel {  Name = "Esterházy Péter", Email = "estike@gmail.com"});

            var recs = db.LoadRecords<UserModel>("Users");
            //foreach(var rec in recs)
            //{
            //    Console.WriteLine(rec.Name);
            //}

            //var rec = db.LoadRecordById<UserModel>("Users", new Guid("29f5a97e-1b8c-4ab7-873c-12e2d26a3a82"));
        
        }
    }
}
