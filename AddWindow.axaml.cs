using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MsBox.Avalonia;
using MySql.Data.MySqlClient;
using TrackList.Entity;

namespace TrackList;

public partial class AddWindow : Window
{
    private AvaloniaList<Track> _tracks;
    private AvaloniaList<Songer> _songers;
    private string _con = "server=localhost;database=cursach;user=root;password=root";
    public AddWindow()
    {
        InitializeComponent();
        FillSonger();
    }
    

     private void AddButton_OnClick(object? sender, RoutedEventArgs e)
     {
         MySqlConnection _connection;
         _tracks = new AvaloniaList<Track>();
         string sql = "insert into Track  (Name, Songer, Duration) values (@Name, @Songer, @Duration)";
         _connection = new MySqlConnection(_con);
         _connection.Open();
         MySqlCommand command = new MySqlCommand(sql, _connection);
         command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = NameTextBox.Text;
         command.Parameters.Add("@Songer", MySqlDbType.Int32).Value = (SongerComboBox.SelectedItem as TrackList.Entity.Songer).ID;
         command.Parameters.Add("@Duration", MySqlDbType.DateTime).Value = DurationDatePicker.SelectedDate;
         command.ExecuteNonQuery();
         _connection.Close();
         TrackWindow trackWindow = new TrackWindow();
         trackWindow.Show();
         MessageAdd();
         this.Close();

     }
     public  async void MessageAdd()
     {
         var box = MessageBoxManager
             .GetMessageBoxStandard("Успешное добавление", "Композиция добавлена в БД");
         var result = await box.ShowAsync();
     }
     public void FillSonger()
     {
         MySqlConnection _connection;
         _songers = new AvaloniaList<Songer>();
         string sql = "select Songer.ID, Songer.Name from cursach.Songer";
         _connection = new MySqlConnection(_con);
         _connection.Open();
         MySqlCommand command = new MySqlCommand(sql, _connection);
         MySqlDataReader reader = command.ExecuteReader();
         while (reader.Read() && reader.HasRows)
         {
             var curSonger = new Songer()
             {
                 ID = reader.GetInt32("ID"),
                 Name = reader.GetString("Name"),


             };
             _songers.Add(curSonger);
         }

         var cb = this.Find<ComboBox>("SongerComboBox");
         cb.ItemsSource = _songers;
         _connection.Close();
     }
}