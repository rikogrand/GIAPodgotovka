using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MsBox.Avalonia;
using MySql.Data.MySqlClient;
using TrackList.Entity;

namespace TrackList;

public partial class DeleteWindow : Window
{
    private Track _selectedTrack;
    private string _con = "server=localhost;database=cursach;user=root;password=root";
    public DeleteWindow(Track selectedTrack)
    {
        _selectedTrack = selectedTrack;
        InitializeComponent();
    }
    

    private void YesButton_OnClick(object? sender, RoutedEventArgs e)
    {
        MySqlConnection _connection;
        string sql = "SET FOREIGN_KEY_CHECKS=0;" + "Delete from Track where ID = @ID LIMIT 1 ";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@ID", MySqlDbType.Int32);
        command.Parameters["@ID"].Value = _selectedTrack.ID;
        command.ExecuteNonQuery();
        _connection.Close();
        MessageDelete();
        this.Close();
        TrackWindow trackWindow = new TrackWindow();
        trackWindow.Show();

    }
    
    public  async void MessageDelete()
    {
        var box = MessageBoxManager
            .GetMessageBoxStandard("Успешное удаление", "Композиция удаление из БД");
        var result = await box.ShowAsync();
    }
}