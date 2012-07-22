using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace King_of_Thieves.Sound
{
    class CAudioPlayer
    {
        private Thread _audioThread;
        private CSound _song;
        private BlockingCollection<CSound> _effects;
        public Dictionary<string, CSound> soundBank = new Dictionary<string, CSound>();

        public CAudioPlayer()
        {
            _effects = new BlockingCollection<CSound>();
            System.Threading.ThreadStart threadStarter = _checkForThingsToPlay;
            _audioThread = new Thread(threadStarter);
            _audioThread.Start();
        }

        ~CAudioPlayer()
        {
            _audioThread.Abort();
            _effects.Dispose();
            _effects = null;
            _song = null;
        }

        public void stop()
        {
            _audioThread.Abort();
        }

        public CSound song
        {
            get
            {
                return _song;
            }
            set
            {
                _song = value;
            }
        }

        public void addSfx(CSound sfx)
        {
            _effects.Add(sfx);
        }

        //this function name is an abomination to my programming abilities. Luckily only the thread is going to use this.
        private void _checkForThingsToPlay()
        {
            while (true)
            {
                _play(_effects.Take());
            }
        }

        private void _play(CSound file)
        {

            if (file.sfx != null)
                file.sfx.Play();
            else if (file.song != null)
            {
                MediaPlayer.IsRepeating = file.loop;
                MediaPlayer.Play(file.song);
            }
            else
                throw new FormatException("The CSound passed did not contain any valid audio information.");
        }
    }
}