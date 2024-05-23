using System;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using MySql.Data.MySqlClient;
using TrackList.Entity;

namespace TrackList;

public partial class MainWindow : Window
{
    private MySqlConnection _connection;
    public AvaloniaList<AuthInfo> _auth;
    private string _con = "server=localhost;database=cursach;user=root;password=root";
    public MainWindow()
    {
      
        InitializeComponent();
    }

    private void LoginButton_OnClick(object? sender, RoutedEventArgs e)
    {
        MySqlConnection _connection;
        _auth = new AvaloniaList<AuthInfo>();
  
        string sql = "select  AuthInfo.Login, AuthInfo.Password from cursach.AuthInfo " +
                     " where Login = @Login and Password = @Password ";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("Login", MySqlDbType.VarChar).Value = LoginTextBox.Text;
        var login = command.Parameters["Login"].Value;
        command.Parameters.Add("Password", MySqlDbType.VarChar).Value = PasswordTextBox.Text;
        var password = command.Parameters["Password"].Value;
        var reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            if (LoginTextBox.Text.Equals(login) && PasswordTextBox.Text.Equals(password))
            {
                TrackWindow trackWindow = new TrackWindow();
                trackWindow.Show();
                MessageLogin();
                this.Close();
            }
            
        }
   
    }

    public  async void MessageLogin()
    {
        var box = MessageBoxManager
            .GetMessageBoxStandard("Успешная авторизация", "Авторизация прошла успешно");
        var result = await box.ShowAsync();
    }
}