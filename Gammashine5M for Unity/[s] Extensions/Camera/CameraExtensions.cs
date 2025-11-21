using UnityEngine;

namespace Snaplight.Extension
{
    public static class CameraExtensions
    {
        public static void Tracking(this Camera camera, Transform target, Vector3 thresholdPosition, Vector3 thresholdRotation)
        {
            //---
            Vector3 desiredPosition = target.position + target.TransformDirection(thresholdPosition);
            camera.transform.position = desiredPosition;

            //---
            Vector3 direction = target.position - camera.transform.position;
            if (direction == Vector3.zero) return;

            //---
            Quaternion lookRotation = Quaternion.LookRotation(target.position - camera.transform.position);
            Quaternion finalRotation = lookRotation * Quaternion.Euler(thresholdRotation);

            //---
            camera.transform.rotation = finalRotation;
        }

        public static void CollisionCheckout(this Camera camera, Transform target, ref Vector3 velocity, float distanceTargetLimitation)
        {
            //---
            Vector3 direction = camera.transform.position - target.position;
            float targetDistance = direction.magnitude;
            direction.Normalize();

            if (Physics.SphereCast(target.position, 0.5f, direction, out RaycastHit hit, targetDistance))
            {
                //---
                float hitDistance = Mathf.Max(hit.distance, distanceTargetLimitation);
                camera.transform.position = target.position + direction * hitDistance;

                //---
                Vector3 slideDirection = Vector3.ProjectOnPlane(direction, hit.normal);
                camera.transform.position = Vector3.SmoothDamp(camera.transform.position, target.position + slideDirection * hitDistance, ref velocity, 0.05f);
            }
        }
    }
}
