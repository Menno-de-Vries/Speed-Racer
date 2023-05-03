using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("Player Data"), order = 0)]

public class PlayerData : ScriptableObject
{
    [Header("The movement and ground check storing point")]
    [Tooltip("Storing point input movement")]
    public Vector3 MovementAxis;
    public bool Grounded = false;

    [Header("The movement settings")]
    [Tooltip("Movement speed Player")]
    public float WalkingSpeed = 10f;
    public float RunningSpeedMultiplier = 2f;

    [Header("The jump and gravity settings")]
    [Tooltip("The gravity so that if the player jumps the gravity will work like the reall gravity in real life")]
    public float Gravity = -9.81f;

    [Tooltip("The jump force for the player when he is jumping")]
    public float JumpForce = 10;

    [Tooltip("The scale to scale the gravity")]
    public float GravityScale = 3f;

}
