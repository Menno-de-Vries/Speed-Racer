using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Settings

    [Tooltip("The speed the car rotates with")]
    public float RotationSpeed = 100f;

    [Tooltip("Player data and info")]
    public PlayerData Data;

    [Tooltip("Rigidbody player")]
    private Rigidbody m_rb;

    #endregion

    #region Standard Methods

    void Start()
    {
        // Gets the rigidbody of the player
        m_rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Grav();
    }

    void FixedUpdate()
    {
        MovePlayer();
        JumpPlayer();
        RotateCar();

        if (transform.rotation.z != 0)
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);

    }

    #endregion

    #region Walking Movement

    private void MovePlayer()
    {
        // Stores the input into the vector3
        Data.MovementAxis = new Vector3(0f, Data.MovementAxis.y, Input.GetAxis("Vertical"));

        // Lets you walk in the direction you are looking in
        Vector3 _move = transform.right * Data.MovementAxis.x + transform.forward * Data.MovementAxis.z;

        // Lets you move the player with the input and the walking speed
        if (!Input.GetKey(KeyCode.LeftShift))
            m_rb.AddForce(_move * Data.WalkingSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        else
            m_rb.AddForce(_move * Data.WalkingSpeed * Data.RunningSpeedMultiplier * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    #endregion

    #region Jumping

    /// <summary>
    /// Makes the player jump if the conditions are met
    /// </summary>
    private void JumpPlayer()
    {
        if (Data.Grounded && Input.GetKey(KeyCode.Space))
        {
            m_rb.AddForce(Vector3.up * Data.JumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
            Data.Grounded = false;
        }
    }

    #endregion

    #region Rotate Car

    private void RotateCar()
    {
        // Calculates the mouse sens multiplied by delta time
        float _RotationSense = RotationSpeed * Time.deltaTime;

        // Stores de input of the mouse axis in the vector3 both axis multiplied by mouseSenseDelta
        Data.MovementAxis = new Vector3(Input.GetAxis("Horizontal") * _RotationSense, Data.MovementAxis.y);

        // Does the movement of the camera
        transform.Rotate(new Vector3(0f, Data.MovementAxis.x));

    }

    #endregion

    #region Gravity

    /// <summary>
    /// This takes the player after jumping back to the ground as gravity should work
    /// </summary>
    private void Grav()
    {
        if (!Data.Grounded)
        {
            // Calculates the gravity that works on the player
            Vector3 _gravity = Data.Gravity * Data.GravityScale * m_rb.mass * Vector3.up;

            // Adds the gravity on to the player
            m_rb.AddForce(_gravity, ForceMode.Acceleration);
        }
    }

    #endregion

    #region Collision Check

    private void OnCollisionStay(Collision _col)
    {
        if (_col.gameObject.GetComponent<GroundTag>())
            Data.Grounded = true;
    }

    private void OnCollisionExit(Collision _col)
    {
        if (_col.gameObject.GetComponent<GroundTag>())
            Data.Grounded = false;
    }

    private void OnTriggerExit(Collider _other)
    {
        GameManager.Instance.CheckAndSafeWaypointLap(_other.gameObject);
    }

    #endregion

}
