using Snaplight.Folds;
using Gammashine.Controllables;

using System;

using UnityEngine;

namespace Gammashine.Modules
{
    public sealed class Autotimer : IAutotimerModulable, IUniversalCallable<Autotimer, int>
    {
        // IAutotimerModulable
        public AutotimerFold Fold { get => _fold; set => _fold = value; }

        [SerializeField] private AutotimerFold _fold;

        // IUniversalCallable
        public int Identifier => UnityEngine.Random.Range(0, int.MaxValue);

        public event Action<Autotimer> Collected;
        public event Action<Autotimer> Eliminated;

        public void Collection()
        {
            Collected?.Invoke(this);
        }

        public void Zeroing()
        {
            Fold.Timer = 0;
            Fold.Controllable = TimedataControllable.Waiting;
        }

        public void Playback()
        {
            float t = Fold.CountdownTypemodel switch
            {
                CountdownTypemodel.Deltatime => Time.deltaTime,
                CountdownTypemodel.Unscaled => Time.unscaledDeltaTime,
                CountdownTypemodel.Realtime => Time.time - Time.realtimeSinceStartup,
                CountdownTypemodel.Platform => DateTime.Now.Second,
                _ => 0f,
            };

            if (Fold.Controllable == TimedataControllable.Finishing) Fold.Controllable = TimedataControllable.Aftereffect;
            if (Fold.Timer >= Fold.Limit) Fold.Controllable = TimedataControllable.Finishing;

            if (Fold.Timer != Fold.Limit) Fold.Timer += t;

            if (Fold.Timer >= Fold.Limit)
            {
                if (Fold.OvertimeTypemodel == OvertimeTypemodel.Stopdown) Fold.Timer = Fold.Limit;
                if (Fold.OvertimeTypemodel == OvertimeTypemodel.Unrestricted) Fold.Timer += t;
                if (Fold.OvertimeTypemodel == OvertimeTypemodel.Loopback) Fold.Timer = 0;
                if (Fold.OvertimeTypemodel == OvertimeTypemodel.Loopback)
                {
                    if (Fold.Timer >= Fold.Limit) Fold.Timer -= t;
                    if (Fold.Timer <= 0) Fold.Timer += t;
                }
            }
        }

        public void Shutdown()
        {
            Fold.Controllable = TimedataControllable.Shutdown;
        }

        public void Changeover(AutotimerChangeover controllable)
        {
            if (controllable == AutotimerChangeover.Playback) Fold.Controllable = TimedataControllable.Playback;
            if (controllable == AutotimerChangeover.Shutdown) Fold.Controllable = TimedataControllable.Shutdown;
            if (controllable == AutotimerChangeover.Zeroing) Fold.Timer = 0;
            if (controllable == AutotimerChangeover.Finishing) Fold.Timer = Fold.Limit;
        }

        public void Elimination()
        {
            Eliminated?.Invoke(this);
        }
    }
}