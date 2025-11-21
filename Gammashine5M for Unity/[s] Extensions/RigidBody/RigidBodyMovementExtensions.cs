using UnityEngine;

namespace Snaplight.Extension
{
    public static partial class RigidBodyExtensions
    {
        private const float KmHInMpH = 1.609F;
        private const float KmHInMs = 3.6F;

        public static Vector3 InputToHorizontalMovement(this Rigidbody rb, Vector2 input)
            => new(input.x, 0, input.y);

        public static Vector3 InputToHorizontalMovementNormalized(this Rigidbody rb, Vector2 input)
            => InputToHorizontalMovement(rb, input).normalized;

        public static void Acceleration(this Rigidbody rb, Vector3 movement, float accelerate)
        {
            Vector3 V3 = rb.transform.TransformDirection(movement);

            rb.velocity += V3 * Time.fixedDeltaTime * accelerate;
        }

        public static void Deceleration(this Rigidbody rb, float decelerate)
        {
            float limit = Mathf.Lerp(rb.velocity.magnitude, 0, decelerate * Time.deltaTime);

            HorizontalVelocityLimitation(rb, limit);
        }

        public static void Gravity(this Rigidbody rb, float gravityVelocity)
        {
            rb.velocity.Set(rb.velocity.x, gravityVelocity, rb.velocity.z);
        }

        public static void HorizontalVelocityLimitation(this Rigidbody rb, float limit)
        {
            float sqrMagnitude = (rb.velocity.x * rb.velocity.x) + (rb.velocity.z * rb.velocity.z);

            if (sqrMagnitude > limit * limit)
            {
                float magnitude = Mathf.Sqrt(sqrMagnitude);
                float ratio = limit / magnitude;

                rb.velocity = new Vector3(rb.velocity.x * ratio, rb.velocity.y, rb.velocity.z * ratio);
            }
        }

        public static void VerticalVelocityLimitation(this Rigidbody rb, float limit)
        {
            float sqrMagnitude = rb.velocity.y * rb.velocity.y;

            if (sqrMagnitude > limit * limit)
            {
                float magnitude = Mathf.Sqrt(sqrMagnitude);
                float ratio = limit / magnitude;

                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * ratio, rb.velocity.z);
            }
        }

        public static void VelocityLimitation(this Rigidbody rb, float horizontalLimit, float verticalLimit)
        {
            HorizontalVelocityLimitation(rb, horizontalLimit);
            VerticalVelocityLimitation(rb, verticalLimit);
        }
    }
}
