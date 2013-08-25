using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionClock.Models
{
    public class Sounds
    {
        public static List<Sound> GetSounds()
        {
            return new List<Sound>
            {
                new Sound
                {
                     Name="Ringtone01",
                     Uri= new Uri("/Sounds/Ringtone01.wma", UriKind.Relative)
                },
                new Sound
                {
                     Name="Ringtone02",
                     Uri= new Uri("/Sounds/Ringtone02.mp3", UriKind.Relative)
                },
                new Sound
                {
                     Name="Ringtone03",
                     Uri= new Uri("/Sounds/Ringtone03.mp3", UriKind.Relative)

                },
                new Sound
                {
                     Name="Ringtone04",
                     Uri= new Uri("/Sounds/Ringtone04.mp3", UriKind.Relative)
                },
                new Sound
                {
                     Name="Ringtone05",
                     Uri=  new Uri("/Sounds/Ringtone05.mp3", UriKind.Relative)
                },
                new Sound
                {
                     Name="Ringtone06",
                     Uri=  new Uri("/Sounds/Ringtone06.wav", UriKind.Relative)
                },
                 new Sound
                {
                     Name="Ringtone07",
                     Uri= new Uri("/Sounds/Ringtone07.wav", UriKind.Relative)
                }
            };
        }
    }
}
