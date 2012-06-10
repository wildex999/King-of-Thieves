using System;
using System.Collections.Generic;
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
        private Queue<CSound> _effects;

        public CAudioPlayer()
        {
            _effects = new Queue<CSound>();
            System.Threading.ThreadStart threadStarter = _checkForThingsToPlay;
            _audioThread = new Thread(threadStarter);
            _audioThread.Start();
        }

        ~CAudioPlayer()
        {
            _audioThread.Abort();
            _effects.Clear();
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

        public CSound sfx
        {
            get
            {
                return _effects.Peek();
            }
        }

        public void addSfx(CSound sfx)
        {
            _effects.Enqueue(sfx);
        }

        //this function name is an abomination to my programming abilities.  Luckily only the thread is going to use this.
        private void _checkForThingsToPlay()
        {
            while (true)
            {
                if (_song != null)
                {
                    _playSong(_song);
                    _song = null;
                }

                while (_effects.Count > 0)
                    _playSfx(_effects.Dequeue());

                Thread.Sleep(100);
                
            }
        }


        private void _playSong(CSound song)
        {
            if (song.song != null)
            {
                MediaPlayer.IsRepeating = song.loop;
                MediaPlayer.Play(song.song);
            }
            else
                throw new FormatException("The CSound passed did not contain any song information.");
        }

        private void _playSfx(CSound sfx)
        {
            if (sfx.sfx != null)
            {
                sfx.sfx.Play();
            }
            else
                throw new FormatException("The CSound passed did not contain any sfx information.");
        }
    }
}
