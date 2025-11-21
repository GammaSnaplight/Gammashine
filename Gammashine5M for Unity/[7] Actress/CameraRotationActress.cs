//using System;

//using UnityEngine;

//using Snaplight.Controllable;

//namespace Snaplight
//{
//    [Serializable]
//    public class FPCRotationActressFold : IFoldables<FPCRotationActressFold>
//    {
//        public MonophaseControllable InputInvertPhase;
//        public MonophaseControllable RotationSmoothnessPhase;

//        public float SensitivityX;
//        public float SensitivityY;

//        public float DownLimitation;
//        public float UpLimitation;

//        public float RotationSmoothness;

//        [HideInInspector] public Quaternion QuaternionPitch;
//        [HideInInspector] public Quaternion QuaternionYaw;

//        [HideInInspector] public float Pitch;
//        [HideInInspector] public float Yaw;
//    }

//    public class CameraRotationActress : ISyncActress<(Quaternion, Quaternion), FPCRotationActressFold, Vector2>
//    {
//        public Vector2 Sync { get; set; }

//        public (Quaternion, Quaternion) Act(FPCRotationActressFold fold)
//        {
//            if (fold.InputInvertPhase == MonophaseControllable.Activeness) new Vector3(Sync.x, Sync.y, 0);
//            else new Vector3(Sync.x, -Sync.y, 0);

//            fold.Pitch += Sync.y * fold.SensitivityX;
//            fold.Yaw += Sync.x * fold.SensitivityY;

//            fold.Pitch = Mathf.Clamp(fold.Pitch, fold.DownLimitation, fold.UpLimitation);

//            Quaternion x = Quaternion.AngleAxis(fold.Pitch, Vector3.right);
//            Quaternion y = Quaternion.AngleAxis(fold.Yaw, Vector3.up);

//            if (fold.RotationSmoothnessPhase == MonophaseControllable.Activeness)
//            {
//                fold.QuaternionPitch = Quaternion.Lerp(fold.QuaternionPitch, x, Time.deltaTime * fold.RotationSmoothness);
//                fold.QuaternionYaw = Quaternion.Lerp(fold.QuaternionYaw, y, Time.deltaTime * fold.RotationSmoothness);
//            }
//            else
//            {
//                fold.QuaternionPitch = x;
//                fold.QuaternionYaw = y;
//            }

//            return (fold.QuaternionPitch, fold.QuaternionYaw);
//        }
//    }
//}

