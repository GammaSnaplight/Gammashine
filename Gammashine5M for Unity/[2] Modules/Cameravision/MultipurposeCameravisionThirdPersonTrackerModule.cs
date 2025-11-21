
using Snaplight.Folds;
using Snaplight.Folds.Scriptable;
using Snaplight.Extension;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace Gammashine.Modules
{
	[Serializable]
    public class MultipurposeCameravisionThirdPersonTrackerModule : IMainstreamModulable
    {
		// IMainstreamModulable
        public ModuleManifold Changeover { get; set; } = new();

        // Serializable: Fold
        public MultipurposeCameravisionThirdPersonTrackerFold Fold = new();

        [HideInInspector] public CameravisionFold CameravisionBindfold;
        [HideInInspector] public MultipurposeCameravisionRotationScriptable ScriptableBindfold;

        // Serializable: Bindfold

        // Serializable: Information

        // Variable
        private Vector3 _velocity;

        public void Collection()
        {
			//---
			Changeover.Undertaking = ModuleUndertaking.Shutdown;
			Changeover.Liabilities = ModuleLiabilities.Regular;
			Changeover.Perfomance = PerfomanceControllable.Superhigh;
			Changeover.Updating = UpdateControllable.Late;

            //---
            
        }

        public void Playback()
        {
            //---
            CameravisionBindfold.Cameravision.transform.LookAt(Fold.ObjectLook.transform);

            CameravisionBindfold.Cameravision.transform.position 
                = Vector3.Lerp(
                    CameravisionBindfold.Cameravision.transform.position,
                    Fold.ObjectLook.transform.position,
                    Time.deltaTime * 20);

            if (Fold.IsCollision) CameravisionBindfold.Cameravision.CollisionCheckout(
                Fold.ObjectLook.transform,
                ref _velocity,
                Fold.DistanceTargetLimitation);
        }

        public void Shutdown()
        {
			//---
            
        }

        public void Lightback()
        {
			//---
            
        }

        public void Elimination()
        {
			//---
            
        }
    }
}