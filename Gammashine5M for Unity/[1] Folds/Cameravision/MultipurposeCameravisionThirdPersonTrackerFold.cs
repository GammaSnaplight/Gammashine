using Gammashine.Bindfolds.Unity.Editor;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace Snaplight.Folds
{
	[Serializable]
	public class MultipurposeCameravisionThirdPersonTrackerFold
	{
		// Serializable
		[VMe("Look At")]
		public GameObject ObjectLook;

		[VMe("Options")]
		public Vector3 ThresholdPosition;
		public Vector3 ThresholdAngle;

		[VMe("Special")]
		public bool IsCollision;
		public float DistanceTargetLimitation;

        // Hide

    }
}
