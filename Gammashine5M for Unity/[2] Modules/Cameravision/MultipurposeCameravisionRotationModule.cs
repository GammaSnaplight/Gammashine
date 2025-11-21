using Snaplight.Folds;
using Snaplight.Folds.Scriptable;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace Gammashine.Modules
{
    [Serializable]
    public class MultipurposeCameravisionRotationModule : IMainstreamModulable
    {
        // IMainstreamModulable
        public ModuleManifold Changeover { get; set; } = new();

        // Serializable
        public MultipurposeCameravisionRotationScriptable Scriptable;

        [HideInInspector] public CameravisionFold CameravisionBindfold;

        [HideInInspector] public MultipurposeCameravisionRotationEnterfold Enterfold;
        [HideInInspector] public MultipurposeCameravisionRotationInformation RotationInformation;

        // Variable
        private Quaternion _smoothnessPitch = Quaternion.identity;
        private Quaternion _smoothnessYaw = Quaternion.identity;

        private float _currentPitchAngle;
        private float _currentYawAngle;

        public void Collection()
        {
			//---
			Changeover.Undertaking = ModuleUndertaking.Playback;
			Changeover.Liabilities = ModuleLiabilities.Regular;
			Changeover.Perfomance = PerfomanceControllable.Non;
			Changeover.Updating = UpdateControllable.Late;
		
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

            _currentPitchAngle = Mathf.Clamp(_currentPitchAngle, Scriptable.RotationYLimitation, Scriptable.RotationNegativeYLimitation);

            //---
            Quaternion pitch = Quaternion.AngleAxis(_currentPitchAngle, Vector3.right);
            Quaternion yaw = Quaternion.AngleAxis(_currentYawAngle, Vector3.up);

            if (Scriptable.IsSmoothness)
            {
                _smoothnessPitch = Quaternion.Lerp(_smoothnessPitch, pitch, Time.deltaTime * Scriptable.RotationSmoothness);
                _smoothnessYaw = Quaternion.Lerp(_smoothnessYaw, yaw, Time.deltaTime * Scriptable.RotationSmoothness);

                RotationInformation.Current = _smoothnessYaw * _smoothnessPitch;

                //---
                CameravisionBindfold.Cameravision.transform.rotation = RotationInformation.Current;
            }
            else
            {
                RotationInformation.Current = yaw * pitch;

                //---
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