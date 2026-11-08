using UnityEngine;

namespace PianoTiles
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] ChartRunner chart;
        [SerializeField] AudioSource source;
        public double ScheduledAt { get; private set; }

        void OnEnable()
        {
            chart.SectionStarted += Play;
            chart.Stopped += source.Stop;
        }

        void OnDisable()
        {
            chart.SectionStarted -= Play;
            chart.Stopped -= source.Stop;
            source.Stop();
        }

        void Play(SongDefinition song, double at)
        {
            source.Stop();
            source.clip = song.audioClip;
            ScheduledAt = at;
            if (source.clip == null) return;
            source.PlayScheduled(at);
            source.SetScheduledEndTime(at + song.duration);
        }
    }
}
