using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.IO;
using static Heretic.Core.NPC;

namespace Heretic.Core
{
    internal class Sound
    {
        private const string SOUNDS_PATH = @"Sounds";
        private const string MUSIC_PATH = @"Music";

        public Dictionary<EnemyType, SoundEffects> soundLibrary;

        private SoundEffect elvenWand;
        public SoundEffect ElvenWand
        {
            get
            {
                return elvenWand;
            }
        }        

        private SoundEffect playerPain;
        public SoundEffect PlayerPain
        {
            get
            {
                return playerPain;
            }
        }

        private SoundEffect playerDeath;
        public SoundEffect PlayerDeath
        {
            get
            {
                return playerDeath;
            }
        }

        public Sound(ContentManager content)
        {
            elvenWand = content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "GLDHIT"));
            
            playerPain = content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "PLRPAI"));
            playerDeath = content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "PLRWDTH"));

            soundLibrary = new Dictionary<EnemyType, SoundEffects>();
            soundLibrary.Add(EnemyType.IMP, new SoundEffects(content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "IMPPAI")), content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "IMPDTH")), content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "IMPAT2"))));
            soundLibrary.Add(EnemyType.MUMMY, new SoundEffects(content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "MUMPAI")), content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "MUMDTH")), content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "MUMAT2"))));
            soundLibrary.Add(EnemyType.KNIGHT, new SoundEffects(content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "KGTPAI")), content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "KGTDTH")), content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "KGTAT2"))));
            soundLibrary.Add(EnemyType.MALOTAUR, new SoundEffects(content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "MINPAI")), content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "MINDTH")), content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "MINAT3"))));

            Song music = content.Load<Song>(Path.Combine(MUSIC_PATH, "mus_e1m1"));
            MediaPlayer.Volume = 0.4f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(music);
        }

        public SoundEffect NPCPain(EnemyType enemyType)
        {
            return soundLibrary[enemyType].Pain;
        }

        public SoundEffect NPCDeath(EnemyType enemyType)
        {
            return soundLibrary[enemyType].Death;
        }

        public SoundEffect NPCAttack(EnemyType enemyType)
        {
            return soundLibrary[enemyType].Attack;
        }

        public struct SoundEffects
        {
            public SoundEffect Pain { get; set; }
            public SoundEffect Death { get; set; }
            public SoundEffect Attack { get; set; }

            public SoundEffects(SoundEffect pain, SoundEffect death, SoundEffect attack)
            {
                Pain = pain;
                Death = death;
                Attack = attack;
            }
        }
    }
}
