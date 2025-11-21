using Gammashine;

using Snaplight.Folds.Scriptable;
using Snaplight.Folds;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace Gammashine.Modules
{
    [Serializable]
    public class MultipurposeCameravisionRotationModule_v2 : IMainstreamModulable
    {
        // IMainstreamModulable
        public ModuleManifold Changeover { get; set; } = new();

        // Serializable

        public MultipurposeCameravisionRotationScriptable Scriptable;

        [HideInInspector] public CameravisionFold CameravisionBindfold;

        [HideInInspector] public MultipurposeCameravisionRotationEnterfold Enterfold;

        [HideInInspector] public MultipurposeCameravisionRotationInformation RotationInformation;

        // Variable
        private float _currentPitchAngle;
        private float _currentYawAngle;

        public void Collection()
        {
			//---
			Changeover.Undertaking = ModuleUndertaking.Shutdown;
			Changeover.Liabilities = ModuleLiabilities.Regular;
			Changeover.Perfomance = PerfomanceControllable.Non;
			Changeover.Updating = UpdateControllable.Update;
		
			//---
            
        }

        public void Playback()
        {
            //---
            Vector3 input = Scriptable.IsInvert
                ? new Vector3(Enterfold.Input.x, Enterfold.Input.y, 0)
                : new Vector3(Enterfold.Input.x, -Enterfold.Input.y, 0);

            //---
            _currentPitchAngle += input.y * Scriptable.SensitivityY;
            _currentYawAngle += input.x * Scriptable.SensitivityX;

            //---
            _currentPitchAngle = Mathf.Clamp(_currentPitchAngle, Scriptable.RotationNegativeYLimitation, Scriptable.RotationYLimitation);

            //---
            Quaternion rotation = Quaternion.Euler(_currentPitchAngle, _currentYawAngle, 0);

            //---
            if (Scriptable.IsSmoothness)
            {
                RotationInformation.Current
                         = Quaternion.Lerp(CameravisionBindfold.Cameravision.transform.rotation, rotation, Time.deltaTime * Scriptable.RotationSmoothness);

                CameravisionBindfold.Cameravision.transform.rotation = RotationInformation.Current;
            }             
            else
            {
                RotationInformation.Current = rotation;

                CameravisionBindfold.Cameravision.transform.rotation = RotationInformation.Current;
            }
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