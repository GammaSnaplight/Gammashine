using UnityEngine;

namespace Snaplight.Extension
{
    public static partial class RigidBodyExtensions
    {
        public static float Angle(this Rigidbody rb, Collider collider, float raytouchLength)
        {
            Vector3 raytouchDirection = Vector3.down;
            Vector3 raytouchPosition = collider.bounds.center - new Vector3(0, collider.bounds.extents.y, 0);

            if (Physics.Raycast(raytouchPosition, raytouchDirection, out RaycastHit hitInfo, raytouchLength))
            {
                Vector3 surfaceNormal = hitInfo.normal;

                return Vector3.Angle(surfaceNormal, Vector3.up);
            }
            else return Constants.ERROR;
        }

        public static Vector3 MovementSurfaceAngle(this Rigidbody rb, Vector3 movement, Vector3 directionRaytouch, Vector3 surfaceRaytouch)
        {
            Vector3 lastKnownSurfaceNormal = Vector3.up;

            if (Physics.Raycast(rb.position, directionRaytouch, out RaycastHit hit))
            {
                lastKnownSurfaceNormal = rb.transform.InverseTransformDirection(hit.normal);
            }

            Quaternion forceRotation = Quaternion.FromToRotation(surfaceRaytouch, lastKnownSurfaceNormal);

            return forceRotation * movement;
        }

        public static Vector3 MovementSurfaceAngleRaw(this Rigidbody rb, Vector3 movement, Vector3 directionRaytouch, Vector3 surfaceRaytouch)
        {
            if (Physics.Raycast(rb.position, directionRaytouch, out RaycastHit hit))
            {
                Vector3 checkoutSurfaceRaytouch = rb.transform.InverseTransformDirection(hit.normal);

                Quaternion forceRotation = Quaternion.FromToRotation(surfaceRaytouch, checkoutSurfaceRaytouch);

                return forceRotation * movement;
            }

            return movement;
        }
    }
}
