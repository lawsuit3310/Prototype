using UnityEngine;

namespace VinChaud
{
    public class AWord
    {
        public Sprite portrait;
        public string speaker;
        public string talk;

        public AWord(Sprite portrait, string speaker, string talk)
        {
            this.portrait = portrait;
            this.speaker = speaker;
            this.talk = talk;
        }
    }
}