using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KS = Assets.GameLogic.Input.InputKeyState;

[RequireComponent(typeof(Camera), typeof(CameraControllerManager))]
public class DefaultCameraController : _BaseCameraController
{
    //==============================================================================
    //
    //                                    CONSTANTS
    //
    //==============================================================================



    public enum ZoomState
    {
        Neutral,
        ZoomingIn,
        ZoomedIn,
        ZoomingOut
    }




    //==============================================================================
    //
    //                                    PROPERTIES
    //
    //==============================================================================



    /// <summary>
    /// The distance in meters that the camera controller moves every frame
    /// </summary>
    [Range(0f, 100)]
    [Header("Velocity")]
    public float Move_Speed = 0.01f;



    /// <summary>
    /// Maximum speed that the camera can travel in Meters per second
    /// </summary>
    [Range(0.01f, 500f)]
    public float MaximumSpeed = 10f;



    /// <summary>
    /// The Current velocity of the camera
    /// </summary>
    public Vector3 Velocity_Current = Vector3.zero;



    /// <summary>
    /// The amount of velocity being applied this frame in Meters per Second
    /// </summary>
    public Vector3 Acceleration_ThisFrame = Vector3.zero;



    /// <summary>
    /// Number of seconds it takes for the camera to come to a full stop on any given axis
    /// </summary>
    public float FullStopTime_Seconds = 1.5f;



    /// <summary>
    /// Stores the time (in seconds) that has passed since the last impulse was received on each axis
    /// </summary>
    protected Vector3 _TimeSinceLastImpulse = new Vector3();



    /// <summary>
    /// Stores the magnitude of current velocity at the time the last impulse was received on a given axis
    /// </summary>
    protected Vector3 _LastImpulseMagnitude = new Vector3();



    /// <summary>
    /// Number of seconds it takes to finish the zoom animation
    /// </summary>
    [Header("Zoom")]
    public float Zoom_ZoomTime = 2f;


    /// <summary>
    /// Multiplier for how far the camera should zoom
    /// </summary>
    [Range(1.01f, 100f)]
    public float Zoom_Magnification = 2f;



    protected float _ZoomFOV_Target = 30f;

    protected float _ZoomFOV_Original = 60f;

    protected float _Zoom_TimeSinceZoomStart = 0f;

    protected ZoomState _Zoom_CurrentState = ZoomState.Neutral;



    [Header("Rotation")]
    public float Rotation_Sensitivity = 3f;


    public float Rotation_TimeToTarget = 1f;



    protected Vector2 _Rotation_Force = Vector2.zero;


    protected Vector2 _Rotation_LastImpulse = Vector2.zero;


    protected Vector2 _Rotation_TimeToTarget = Vector2.zero;




    //==============================================================================
    //
    //                                UNITY LIFECYCLE
    //
    //==============================================================================



    // Use this for initialization
    void Start ()
    {
        this._Camera = this.GetComponent<Camera>();

        //Get Root Camera Object
        if (this.transform.parent != null && this.transform.parent.name == "Camera Root")
        {
            base._XForm_CameraRoot = this.transform.parent;
        }
        else
            Debug.LogError($"Could not find a Camera Root for Camera {this.name}:{this.GetInstanceID()}.");
    }
	


	// Update is called once per frame
	void LateUpdate ()
    {
        this.Acceleration_ThisFrame = Vector3.zero;



        #region Move Forward / Backward

        if (GameLoop.MAIN.InputManager.VerticalAxis.CurrentState == KS.State.Key_Pressed || GameLoop.MAIN.InputManager.VerticalAxis.Negative_CurrentState == KS.State.Key_Pressed)
        {
            //If an impulse is received in the opposite direction, kill the velocity in the other direction based on the dot product
            float dot = Vector3.Dot(this.transform.forward.normalized * (GameLoop.MAIN.InputManager.VerticalAxis.Negative_CurrentState == KS.State.Key_Pressed ? -1f : 1f), this.Velocity_Current.normalized);

            if (dot < 0f)
                this.Velocity_Current.z *= 1 - Mathf.Abs(dot);
        }
        else if (GameLoop.MAIN.InputManager.VerticalAxis.CurrentState == KS.State.Key_HeldDown)
        {
            //Move Forward
            this.Acceleration_ThisFrame += this.transform.forward.normalized * this.Move_Speed;
        }
        else if (GameLoop.MAIN.InputManager.VerticalAxis.Negative_CurrentState == KS.State.Key_HeldDown)
        {
            //Move Backward
            this.Acceleration_ThisFrame += -this.transform.forward.normalized * this.Move_Speed;
        }

        #endregion



        #region Left / Right

        if (GameLoop.MAIN.InputManager.HorizontalAxis.CurrentState == KS.State.Key_Pressed || GameLoop.MAIN.InputManager.HorizontalAxis.Negative_CurrentState == KS.State.Key_Pressed)
        {
            //If an impulse is received in the opposite direction, kill the velocity in the other direction based on the dot product
            float dot = Vector3.Dot(this.transform.right.normalized * (GameLoop.MAIN.InputManager.HorizontalAxis.Negative_CurrentState == KS.State.Key_Pressed ? -1f : 1f), this.Velocity_Current.normalized);

            if (dot < 0f)
                this.Velocity_Current.x *= 1-Mathf.Abs(dot);
        }
        else if (GameLoop.MAIN.InputManager.HorizontalAxis.CurrentState == KS.State.Key_HeldDown)
        {
            //Move Right
            this.Acceleration_ThisFrame += this.transform.right.normalized * this.Move_Speed;
        }
        else if (GameLoop.MAIN.InputManager.HorizontalAxis.Negative_CurrentState == KS.State.Key_HeldDown)
        {
            //Move Left
            this.Acceleration_ThisFrame += -this.transform.right.normalized * this.Move_Speed;
        }

        #endregion


        #region Move Up / Down

        if (GameLoop.MAIN.InputManager.MoveUpDown.CurrentState == KS.State.Key_Pressed || GameLoop.MAIN.InputManager.MoveUpDown.Negative_CurrentState == KS.State.Key_Pressed)
        {
            //If an impulse is received in the opposite direction, kill the velocity in the other direction based on the dot product
            float dot = Vector3.Dot(this.transform.up.normalized * (GameLoop.MAIN.InputManager.MoveUpDown.Negative_CurrentState == KS.State.Key_Pressed ? -1f : 1f), this.Velocity_Current.normalized);

            if (dot < 0f)
                this.Velocity_Current.y *= 1 - Mathf.Abs(dot);
        }
        else if (GameLoop.MAIN.InputManager.MoveUpDown.CurrentState == KS.State.Key_HeldDown)
        {
            //Move Up
            this.Acceleration_ThisFrame += this.transform.up.normalized * this.Move_Speed;
        }
        else if (GameLoop.MAIN.InputManager.MoveUpDown.Negative_CurrentState == KS.State.Key_HeldDown)
        {
            //Move Down
            this.Acceleration_ThisFrame += -this.transform.up.normalized * this.Move_Speed;
        }

        #endregion


        if (GameLoop.MAIN.InputManager.CameraRoll.CurrentState == KS.State.Key_HeldDown)
        {
            //Roll Right
            Debug.Log("Roll Right");
        }
        else if (GameLoop.MAIN.InputManager.CameraRoll.Negative_CurrentState == KS.State.Key_HeldDown)
        {
            //Roll Left
            Debug.Log("Roll Left");
        }

        if(GameLoop.MAIN.InputManager.ResetCameraRotation.CurrentState == KS.State.Key_Pressed)
        {
            //Reset the Camera Rotation
            Debug.Log("Reset the Camera Rotation");
            _UprightCamera();
        }


        if (GameLoop.MAIN.InputManager.ZoomCamera.CurrentState == KS.State.Key_Pressed)
            _ZoomIn_Start();
        else if (GameLoop.MAIN.InputManager.ZoomCamera.CurrentState == KS.State.Key_HeldDown)
            _ZoomIn();
        else if (GameLoop.MAIN.InputManager.ZoomCamera.CurrentState == KS.State.Key_Up)
            _ZoomOut_Start();
        else
            _ZoomOut();

        _MoveCamera();
        _RotateCamera();
    }





    //==============================================================================
    //
    //                                    METHODS
    //
    //==============================================================================



    /// <summary>
    /// Checks each axis of acceleration and if there was no impulse in that direction, then decelerate the velocity in that axis
    /// </summary>
    protected void _Decelerate()
    {
        if (this.Velocity_Current != Vector3.zero)
        {
            if (Mathf.Abs(this.Acceleration_ThisFrame.x) <= 0f)
            {
                this._TimeSinceLastImpulse.x += Time.deltaTime;
                this.Velocity_Current.x = Mathf.Lerp(this._LastImpulseMagnitude.x, 0, Mathf.Pow(Mathf.Clamp01(this._TimeSinceLastImpulse.x / FullStopTime_Seconds), 0.5f));
            }
            else
            {
                this._TimeSinceLastImpulse.x = 0f;
                this._LastImpulseMagnitude.x = this.Velocity_Current.x;
            }

            if (Mathf.Abs(this.Acceleration_ThisFrame.y) <= 0f)
            {
                this._TimeSinceLastImpulse.y += Time.deltaTime;
                this.Velocity_Current.y = Mathf.Lerp(this._LastImpulseMagnitude.y, 0, Mathf.Pow(Mathf.Clamp01(this._TimeSinceLastImpulse.y / FullStopTime_Seconds), 0.5f));
            }
            else
            {
                this._TimeSinceLastImpulse.y = 0f;
                this._LastImpulseMagnitude.y = this.Velocity_Current.y;
            }

            if (Mathf.Abs(this.Acceleration_ThisFrame.z) <= 0f)
            {
                this._TimeSinceLastImpulse.z += Time.deltaTime;
                this.Velocity_Current.z = Mathf.Lerp(this._LastImpulseMagnitude.z, 0, Mathf.Pow(Mathf.Clamp01(this._TimeSinceLastImpulse.z / FullStopTime_Seconds), 0.5f));
            }
            else
            {
                this._TimeSinceLastImpulse.z = 0f;
                this._LastImpulseMagnitude.z = this.Velocity_Current.z;
            }
        }
    }



    /// <summary>
    /// Animates camera movement in the scene
    /// </summary>
    protected void _MoveCamera()
    {
        this.Velocity_Current += this.Acceleration_ThisFrame;
        _Decelerate();

        if(this.Velocity_Current != Vector3.zero && XForm_CameraRoot)
        {
            _EnforceSpeedLimit();
            XForm_CameraRoot.position += this.Velocity_Current;
        }
    }



    /// <summary>
    /// Resets the camera rotation to be oriented at default y up
    /// </summary>
    protected void _UprightCamera()
    {
        if(_XForm_CameraRoot)
        {
            _XForm_CameraRoot.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }


    protected void _RotateCamera()
    {
        this._Rotation_Force.x += Input.GetAxis("Mouse X");
        this._Rotation_Force.y -= Input.GetAxis("Mouse Y");

        //Speed limit
        this._Rotation_Force.x = Mathf.Clamp(this._Rotation_Force.x, -20f, 20f);
        this._Rotation_Force.y = Mathf.Clamp(this._Rotation_Force.y, -20f, 20f);


        //Handle deceleration
        if (Mathf.Abs(Input.GetAxis("Mouse X")) == 0f)
        {
            //decrease rotational force on axis
            _Rotation_TimeToTarget.x -= Time.deltaTime;
            _Rotation_Force.x = Mathf.Lerp(_Rotation_LastImpulse.x, 0, Mathf.Pow(Mathf.Clamp01((Rotation_TimeToTarget - _Rotation_TimeToTarget.x) / Rotation_TimeToTarget), 0.25f));
        }
        else
        {
            _Rotation_TimeToTarget.x = Rotation_TimeToTarget;
            _Rotation_LastImpulse.x = _Rotation_Force.x;
        }

        if (Mathf.Abs(Input.GetAxis("Mouse Y")) == 0f)
        {
            //decrease rotational force on axis
            _Rotation_TimeToTarget.y -= Time.deltaTime;
            _Rotation_Force.y = Mathf.Lerp(_Rotation_LastImpulse.y, 0, Mathf.Pow(Mathf.Clamp01((Rotation_TimeToTarget - _Rotation_TimeToTarget.y) / Rotation_TimeToTarget), 0.25f));
        }
        else
        {
            _Rotation_TimeToTarget.y = Rotation_TimeToTarget;
            _Rotation_LastImpulse.y = _Rotation_Force.y;
        }


        if (_Rotation_Force.x != 0f)
            XForm_CameraRoot.rotation *= Quaternion.AngleAxis((_Rotation_Force.x * Rotation_Sensitivity), Vector3.up);
        if (_Rotation_Force.y != 0f)
            XForm_CameraRoot.rotation *= Quaternion.AngleAxis((_Rotation_Force.y * Rotation_Sensitivity), Vector3.right);
    }



    /// <summary>
    /// Clamps the velocity to set to the maximum speed.
    /// </summary>
    protected void _EnforceSpeedLimit()
    {
        if (this.Velocity_Current.sqrMagnitude > MaximumSpeed * MaximumSpeed)
            this.Velocity_Current = Vector3.ClampMagnitude(this.Velocity_Current, MaximumSpeed);
    }




    /// <summary>
    /// Starts zooming in regardless of state
    /// </summary>
    protected void _ZoomIn_Start()
    {
        //Setup zoom animation information
        if (this._Camera)
        {
            if(this._Zoom_CurrentState == ZoomState.Neutral)
                this._ZoomFOV_Original = this._Camera.fieldOfView;

            this._ZoomFOV_Target = this._ZoomFOV_Original / Zoom_Magnification;
            this._Zoom_TimeSinceZoomStart = 0f;

            this._Zoom_CurrentState = ZoomState.ZoomingIn;
        }
    }



    /// <summary>
    /// Handles the zooming in process
    /// </summary>
    protected void _ZoomIn()
    {
        if(this._Zoom_CurrentState == ZoomState.ZoomingIn)
        {
            //Zoom towards target FOV
            this._Zoom_TimeSinceZoomStart += Time.deltaTime;

            float ZoomPerc = Mathf.Clamp01(Mathf.Pow(this._Zoom_TimeSinceZoomStart / this.Zoom_ZoomTime, 0.4f));

            if (this._Camera)
                this._Camera.fieldOfView = Mathf.Lerp(this._ZoomFOV_Original, this._ZoomFOV_Target, ZoomPerc);

            if (ZoomPerc >= 1f)
                this._Zoom_CurrentState = ZoomState.ZoomedIn;
        }
    }



    /// <summary>
    /// Starts zooming out process regardless of current state
    /// </summary>
    protected void _ZoomOut_Start()
    {
        //Setup zoom animation information
        if (this._Camera)
        {
            this._ZoomFOV_Target = this._Camera.fieldOfView;
            this._Zoom_TimeSinceZoomStart = 0f;

            this._Zoom_CurrentState = ZoomState.ZoomingOut;
        }
    }



    /// <summary>
    /// Handles zooming out process
    /// </summary>
    protected void _ZoomOut()
    {
        if(this._Zoom_CurrentState == ZoomState.ZoomingOut)
        {
            //Zoom back to original state
            this._Zoom_TimeSinceZoomStart += Time.deltaTime;

            float ZoomPerc = Mathf.Clamp01(Mathf.Pow(this._Zoom_TimeSinceZoomStart / this.Zoom_ZoomTime, 0.25f));

            if (this._Camera)
                this._Camera.fieldOfView = Mathf.Lerp(this._ZoomFOV_Target, this._ZoomFOV_Original, ZoomPerc);

            if (ZoomPerc >= 1f)
                this._Zoom_CurrentState = ZoomState.Neutral;
        }
    }
}