/*
 * 
 */
 
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Door : MonoBehaviour {

    //Public Fields
    public enum RotationDirection
    {
        Right,
        Left
    }
    public enum InteractionType
    {
        Click,
        Trigger,
        Raycast
    }
    public enum SpeedType
    {
        Float,
        Time,
        Curve
    }
    public enum HingePosition
    {
        Top,
        Left,
        Bottom,
        Right
    }

    [Header("Door Settings")]
    public HingePosition hingePositionEnum;
    public Vector3 hingePosition;
    public bool visualizeHinge;

    [Header("Interaction Settings")]
    public bool isInteractionActive;
    public Sprite interactionIcon;
    public InteractionType interactionType;

    [Header("Rotation Settings")]
    public float initialAngle;
    public float rotationAngle;
    public float rotationSpeed;
    public float rotationTime;
    public AnimationCurve curve;
    public SpeedType speedType;
    public RotationDirection rotationDirection;

    [Header("Sound Settings")]
    public bool isSoundActive;
    public List<AudioClip> openingSounds = new List<AudioClip>();
    public List<AudioClip> closingSounds = new List<AudioClip>();
    public List<AudioClip> knockSounds = new List<AudioClip>();

    //Private Fields
    private bool isDoorOpen = false;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnMouseDown()
    {
        
    }

}