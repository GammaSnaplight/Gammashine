using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Playables;

namespace Project
{
    public static class TimelineExtensions
    {
        public static void Finish(this PlayableDirector timeline)
        {
            timeline.time = timeline.duration;
            timeline.Evaluate();
            timeline.Stop();
        }

        public static void Playback(this PlayableDirector timeline)
        {
            if (timeline.state != PlayState.Paused)
            {
                timeline.Pause();
            }

            timeline.time += Time.deltaTime;

            if (timeline.time >= timeline.duration)
            {
                timeline.time = timeline.duration;
                timeline.Stop();
                return;
            }

            timeline.DeferredEvaluate();
        }
    }
}
