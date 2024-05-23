using System;
using System.Linq;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using TrackList.Entity;

namespace TrackList;

public partial class TrackWindow : Window
{
    private AvaloniaList<Track> _tracks;
    private AvaloniaList<Songer> _songers;
    private MySqlConnection _connection;
    private string _con = "server=localhost;database=cursach;user=root;password=root";
    public TrackWindow()
    {
        InitializeComponent();
        ShowTable();
  
    }

    public void ShowTable()
    {
        MySqlConnection _connection;
        _tracks = new AvaloniaList<Track>();
        string sql = "select Track.ID, Track.Name, S.Name as Songer, Track.Duration from cursach.Track " +
                     " join Songer S on Track.Songer = S.ID ";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curTrack = new Track()
            {
             ID = reader.GetInt32("ID"),
             Name = reader.GetString("Name"),
             Songer = reader.GetString("Songer"),
             Duration = reader.GetDateTime("Duration"),

            };
            _tracks.Add(curTrack);
        }

        TracksDataGrid.ItemsSource = _tracks;
         _connection.Close();

    }
    
    
    
 private void AddButton_OnClick(object? sender, RoutedEventArgs e)
 {
     AddWindow addWindow = new AddWindow();
     addWindow.Show();
     this.Close();
 }
 
 private void EditButton_OnClick(object? sender, RoutedEventArgs e)
 {
     Track selectedTrack = TracksDataGrid.SelectedItem as Track;
     if (selectedTrack is null)
     {
         return;
     }
     EditWindow editWindow = new EditWindow(selectedTrack);
     editWindow.Show();
     this.Close();
 }

 private void DeleteButton_OnClick(object? sender, RoutedEventArgs e)
 {
     Track selectedTrack = TracksDataGrid.SelectedItem as Track;
     if (selectedTrack is null)
     {
         return;
     }
     DeleteWindow deleteWindow = new DeleteWindow(selectedTrack);
     deleteWindow.Show();
     this.Close();
 }

 private void SearchTextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
 {
     var searchTrack = _tracks.Where(x =>
         x.Name.Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) ||
         x.Songer.Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) ||
         x.Duration.ToString() == SearchTextBox.Text);
     TracksDataGrid.ItemsSource = searchTrack;
     if (SearchTextBox is null) TracksDataGrid.ItemsSource = _tracks;
 }
}