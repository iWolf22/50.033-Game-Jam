namespace PianoTiles
{
    public enum Judgement { Hit, Miss }

    public class NoteJudge
    {
        public NoteData Data { get; }
        public bool Resolved { get; private set; }
        public NoteJudge(NoteData data) => Data = data;

        public bool Press(double time)
        {
            if (Resolved || time < Data.start || time > Data.End) return false;
            Resolved = true;
            return true;
        }

        public bool Expire(double time)
        {
            if (Resolved || time <= Data.End) return false;
            Resolved = true;
            return true;
        }
    }
}
