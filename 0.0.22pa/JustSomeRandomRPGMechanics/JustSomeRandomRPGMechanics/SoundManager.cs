using System;
using System.Collections.Generic;
using System.Media;
using System.IO;

namespace JustSomeRandomRPGMechanics
{
    class SoundManager
    {
        SoundPlayer soundPlayer;
        List<string> soundFilePaths;
        int currentSong = 0;
        public SoundManager()
        {
            soundPlayer = new SoundPlayer();
            soundFilePaths = new List<string>();
        }
        public void FindAllSoundFiles()
        {
            string[] fileEntries = Directory.GetFiles(Directory.GetCurrentDirectory());
            foreach(string file in fileEntries)
            {
                if (file.EndsWith(".wav"))
                    AddSoundFilePath(file);
            }
        }
        public void AddSoundFilePath(string path)
        {
            soundFilePaths.Add(path);
        }
        public void ChooseSound(int index)
        {
            soundPlayer.SoundLocation=soundFilePaths[index];
            currentSong = index;
            try
            {
                Play();
            }
            catch (FileNotFoundException)
            {
                soundPlayer.SoundLocation = soundFilePaths[0];
                currentSong = 0;
                Play();
            }
        }
        public void Play()
        {
            soundPlayer.PlayLooping();
        }
        public void Stop()
        {
            soundPlayer.Stop();
        }
    }
}
