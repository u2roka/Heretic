using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace Heretic.Core
{
    internal class Sound
    {
        private const string SOUNDS_PATH = @"Sounds";

        private SoundEffect elvenWand;
        public SoundEffect ElvenWand
        {
            get
            {
                return elvenWand;
            }
        }

        private SoundEffect npcPain;
        public SoundEffect NPCPain 
        { 
            get
            {
                return npcPain;
            }
        }

        private SoundEffect npcDeath;
        public SoundEffect NPCDeath
        {
            get
            {
                return npcDeath;
            }
        }

        private SoundEffect npcAttack;
        public SoundEffect NPCAttack
        {
            get
            {
                return npcAttack;
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

        public Sound(ContentManager content)
        {
            elvenWand = content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "GLDHIT"));
            npcPain = content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "IMPPAI"));
            npcDeath = content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "IMPDTH"));
            npcAttack = content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "IMPAT2"));
            playerPain = content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, "PLRPAI"));
        }
    }
}
