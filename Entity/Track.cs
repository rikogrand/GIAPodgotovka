using System;
using System.Drawing;
using System.IO;

namespace TrackList.Entity;

public class Track
{
 
  
    public int ID { get; set; }
    public string Name { get; set; }
    public string Songer { get; set; }
    public DateTime Duration { get; set; }
}