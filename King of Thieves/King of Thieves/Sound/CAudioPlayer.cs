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

        //public CSound sfx
        //{
        //    get
        //    {
        //        return _effects.
        //    }
        //}

        public void addSfx(CSound sfx)
        {
            _effects.Add(sfx);
        }

        //this function name is an abomination to my programming abilities. Luckily only the thread is going to use this.
        //I used your cpu usage fix in here.  Keep in mind, this needs to be in a loop so it can catch song changes. -Steve
        private void _checkForThingsToPlay()
        {

            while (true)
            {
                if (_song != null)
                {
                    _playSong(_song);
                    _song = null;
                }

                _playSfx(_effects.Take());
            }

         
        }

        //public void update()
        //{
        //    if (_song != null)
        //    {
        //        _playSong(_song);
        //        _song = null;
        //    }

        //    while (_effects.Count > 0)
        //        _playSfx(_effects.Dequeue());
        //}

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