using System;
using System.IO;
using Avalonia.Media.Imaging;

namespace TrackList.Entity;

public class AuthInfo
{
    public int ID { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
     public byte[] Image { get; set; }
      public Uri Logo => new Uri($"avares://TrackList/Image/{Image}");

public Bitmap bitmap
    {
        get
        {
            using var memoryStream = new MemoryStream(Image);
            return new Bitmap(memoryStream);
        }
    }

}