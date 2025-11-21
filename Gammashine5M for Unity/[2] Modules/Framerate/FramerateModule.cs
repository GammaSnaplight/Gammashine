using System;

using Snaplight.Folds;
using Snaplight.Controllable;

using UnityEngine;

namespace Gammashine.Modules
{
    [Serializable]
    public class FramerateModule : IMainstreamModulable
    {
        // IMainstream
        public ModuleManifold Changeover { get; set; } = new();

        // Serializable
        public FramerateFold Fold = new();

        // Serializable: Information
        public FramerateInformation FramerateInformation;

        // Variable

        public void Collection()
        {
            //---
            Changeover.Undertaking = ModuleUndertaking.Playback;
            Changeover.Perfomance = PerfomanceControllable.Non;
            Changeover.Updating = UpdateControllable.Update;
            Changeover.Liabilities = ModuleLiabilities.Regular;

            //---
            Fold.FPSArray = new byte[Fold.FramerateAverageLimitation];
        }

        public void Playback()
        {
            //---
            //Fold.FPS = ((byte)(1 / Time.unscaledDeltaTime));

            //Fold.FPSAverage = Mathlight.Average(Fold.FPSArray);
            //Fold.FPSMinimal = Mathlight.ApximlNum(0, Fold.FPSArray);
            //Fold.FPSMaximum = Mathlight.ApximlNum(byte.MaxValue, Fold.FPSArray);

            ////
            //Fold.IndexArray++;

            //Fold.IndexArray = Mathlight.Maximum(Fold.IndexArray, Fold.FPSAverage - 1);

            //Fold.FPSArray[Fold.IndexArray] = Fold.FPS;

            //if (Fold.IndexArray > Fold.FramerateAverageLimitation) Fold.IndexArray = 0;

            ////---
            //FramerateInformation.FPS = Fold.FPS;
        }

        public void Shutdown()
        {
            //---
            Fold.IndexArray = 0;
        }

        public void Lightback()
        {
            //---
            FramerateInformation.FPS = ((byte)(1 / Time.unscaledDeltaTime));
        }

        public void Elimination()
        {
            
        }
    }
}
