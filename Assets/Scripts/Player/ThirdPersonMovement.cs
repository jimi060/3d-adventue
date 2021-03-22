using UnityEngine;

/// <summary>
///     Class to handle third person movement
/// </summary>
public class ThirdPersonMovement : MonoBehaviour
{
    // Public

    /// <summary>
    ///     The controller for the character
    /// </summary>
    public CharacterController Controller;

    /// <summary>
    ///     The third person camera
    /// </summary>
    public Transform TargetCamera;

    /// <summary>
    ///     The speed the character can move at
    /// </summary>
    public float Speed;

    /// <summary>
    ///     The smothing to apply to turning
    /// </summary>
    public float SmoothTime;

    // Private

    /// <summary>
    ///     Reference variable used by SmoothDampAngle
    /// </summary>
    private float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        float horizonal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizonal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + TargetCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, SmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Controller.Move(moveDirection.normalized * Speed * Time.deltaTime);
        }
    }
}
