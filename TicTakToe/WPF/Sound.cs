using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace TicTakToe.WPF
{
    class Sound
    {
        private void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer("/Images/Sound.wav");
            simpleSound.Play();
        }

    }
}

