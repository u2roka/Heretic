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

        public Sound(ContentManager content, string path)
        {
            elvenWand = content.Load<SoundEffect>(Path.Combine(SOUNDS_PATH, path));
        }
    }
}
