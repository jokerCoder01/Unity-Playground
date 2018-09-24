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
        Right = -1,
        Left = 1
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
    public float curveFactor;
    public AnimationCurve curve;
    public SpeedType speedType;
    public RotationDirection rotationDirection;

    [Header("Sound Settings")]
    public bool isSoundActive;
    public List<AudioClip> openingSounds = new List<AudioClip>();
    public List<AudioClip> closingSounds = new List<AudioClip>();
    public List<AudioClip> knockSounds = new List<AudioClip>();

    //Private Fields
    private Transform trans;
    private bool isDoorOpen = false;
    private bool isDoorInteracted = false;
    private float _direction;
    private float _rotation;
    private float _rotationAngle;
    private float _rotationTime;
    private float curveTimer = 0f;
    private Vector3 rotationAxis;


    void Start()
    {
        trans = GetComponent<Transform>();
        _direction = rotationDirection == RotationDirection.Left ? 1f : -1f;
        _rotationAngle = rotationAngle;
        _rotationTime = rotationTime;

        if (hingePositionEnum == HingePosition.Left || hingePositionEnum == HingePosition.Right)
            rotationAxis = Vector3.up;
        else if (hingePositionEnum == HingePosition.Top || hingePositionEnum == HingePosition.Bottom)
            rotationAxis = Vector3.right;

        if (isSoundActive == true)
            gameObject.AddComponent<AudioSource>();
        else
        {
            if(gameObject.GetComponent<AudioSource>() != null)
            {
                Destroy(GetComponent<AudioSource>());
            }
        }
    }

    void Update()
    {
        if (isDoorInteracted == true && isDoorOpen == false)
        {
            RotateDoor(1f, "Door OPENED!", true);
        }
        else if(isDoorInteracted == true && isDoorOpen == true)
        {
            RotateDoor(-1f, "Door CLOSED!", false);
        }
    }

    //TEMP
    void OnMouseDown()
    {
        if(isInteractionActive == true && interactionType == InteractionType.Click)
        {
            isDoorInteracted = true;
            if (isDoorOpen == false)
                PlaySound(1f);
            else
                PlaySound(-1f);
        }
            
    }

    private void RotateDoor(float direction, string debugMessage, bool _isDoorOpen)
    {
        switch (speedType)
        {
            case SpeedType.Float:
                _rotation = rotationSpeed * _direction * Time.deltaTime;
                _rotationAngle -= _rotation;
                Rotate(direction, debugMessage, _isDoorOpen);
                break;
            case SpeedType.Time:
                _rotation = (_rotationAngle / _rotationTime) * _direction * Time.deltaTime;
                _rotationAngle -= _rotation;
                _rotationTime -= Time.deltaTime;
                Rotate(direction, debugMessage, _isDoorOpen);
                break;
            case SpeedType.Curve:
                curveTimer += Time.deltaTime;
                _rotation = curve.Evaluate(curveTimer) * curveFactor * _direction * Time.deltaTime;
                _rotationAngle -= _rotation;
                Rotate(direction, debugMessage, _isDoorOpen);
                break;
        }

    }

    private void Rotate(float revert, string debugMessage, bool _isDoorOpen)
    {
        switch (rotationDirection)
        {
            case RotationDirection.Left:
                if (_rotationAngle > 0)
                {
                    trans.RotateAround(hingePosition, rotationAxis, _rotation * revert);
                }
                else
                {
                    isDoorOpen = _isDoorOpen;
                    isDoorInteracted = false;
                    _rotationAngle = rotationAngle;
                    _rotationTime = rotationTime;
                    curveTimer = 0f;
                    Debug.Log(debugMessage);
                }
                break;
            case RotationDirection.Right:
                if (_rotationAngle < rotationAngle * 2)
                {
                    trans.RotateAround(hingePosition, rotationAxis, _rotation * revert);
                }
                else
                {
                    isDoorOpen = _isDoorOpen;
                    isDoorInteracted = false;
                    _rotationAngle = rotationAngle;
                    _rotationTime = rotationTime;
                    curveTimer = 0f;
                    Debug.Log(debugMessage);
                }
                break;
        }
    }

    private void PlaySound(float soundType)
    {
        //Opening Sounds
        if(soundType == 1f && openingSounds.Count > 0)
        {
            int rand = Random.Range(0, openingSounds.Count);
            Debug.Log(rand);
            GetComponent<AudioSource>().PlayOneShot(openingSounds[rand]);
        }
        //Closing Sounds
        else if(soundType == -1f && closingSounds.Count > 0)
        {
            int rand = Random.Range(0, closingSounds.Count);
            GetComponent<AudioSource>().PlayOneShot(closingSounds[rand]);
        }
        //Knock Sounds
        else if(soundType == 0f && knockSounds.Count > 0)
        {
            int rand = Random.Range(0, knockSounds.Count);
            GetComponent<AudioSource>().PlayOneShot(knockSounds[rand]);
        }
    }

}