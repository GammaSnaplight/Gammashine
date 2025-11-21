using Gammashine.Controllables;
using Gammashine.Stationary.Mathematics;

using System;

using Snaplight.Folds;
using Snaplight.Controllable;

using UnityEngine;

namespace Gammashine.Modules
{
    [Serializable]
    public class FrameratePerformanceModule : IMainstreamModulable
    {
        // IMainstream
        public ModuleManifold Changeover { get; set; } = new();

        // Serializable
        public FrameratePerformanceFold Fold = new();

        // Serializable: Bind
        [HideInInspector] public FramerateInformation FramerateBind;

        // Serializable: Information
        [HideInInspector] public FrameratePerformanceControllable PerformanceControllableInformation;

        // Variable

        public void Collection()
        {
            Changeover.Updating = UpdateControllable.Update;

            PerformanceControllableInformation = FrameratePerformanceControllable.Medium;
        }

        public void Playback()
        {
            //===
            if (Fold.FramerateCheckoutTimedata.OvertimeTypemodel != OvertimeTypemodel.Loopback) PerformanceControllableInformation = FrameratePerformanceControllable.Error;

            //===
            if (!Mathlight.Apximl(FramerateBind.FPS, Fold.StandardFramerate, Fold.ThresholdFramerate)) Fold.FramerateCheckoutTimedata.Zeroing();

            //===
            if (FramerateBind.FPS < Fold.StandardFramerate - Fold.ThresholdFramerate)
            {
                Fold.FramerateCheckoutTimedata.Playback();

                if (Fold.FramerateCheckoutTimedata.IsOvertime) PerformanceControllableInformation--;
            }
            if (FramerateBind.FPS > Fold.StandardFramerate - Fold.ThresholdFramerate)
            {
                Fold.FramerateCheckoutTimedata.Playback();

                if (Fold.FramerateCheckoutTimedata.IsOvertime) PerformanceControllableInformation++;
            }

            if (PerformanceControllableInformation < FrameratePerformanceControllable.Underlow) PerformanceControllableInformation = FrameratePerformanceControllable.Underlow;
            if (PerformanceControllableInformation > FrameratePerformanceControllable.Super) PerformanceControllableInformation = FrameratePerformanceControllable.Super;
        }

        public void Shutdown()
        {
            Fold.FramerateCheckoutTimedata.Zeroing();
        }

        public void Lightback()
        {

        }

        public void Elimination()
        {

        }
    }
}
