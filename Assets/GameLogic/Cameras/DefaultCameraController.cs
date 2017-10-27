using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KS = Assets.GameLogic.Input.InputKeyState;

[RequireComponent(typeof(Camera), typeof(CameraControllerManager))]
public class DefaultCameraController : _BaseCameraController
{





    //==============================================================================
    //
    //                                    PROPERTIES
    //
    //==============================================================================



    /// <summary>
    /// The distance in meters that the camera controller moves every frame
    /// </summary>
    [Range(0f, 5000)]
    public float Move_Speed = 5f;







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
            base._XForm_CameraRoot = this.transform.parent;
        else
            Debug.LogError($"Could not find a Camera Root for Camera {this.name}:{this.GetInstanceID()}.");


    }
	


	// Update is called once per frame
	void LateUpdate ()
    {
        if (GameLoop.MAIN.InputManager.VerticalAxis.CurrentState == KS.State.Key_HeldDown)
        {
            //Move Forward
            Debug.Log("Move Forward");
            base._XForm_CameraRoot.Translate(new Vector3(0, 0, Move_Speed * Time.deltaTime), Space.Self);
        }
        else if (GameLoop.MAIN.InputManager.VerticalAxis.Negative_CurrentState == KS.State.Key_HeldDown)
        {
            //Move Backward
            Debug.Log("Move Backward");
            base._XForm_CameraRoot.Translate(new Vector3(0, 0, -Move_Speed * Time.deltaTime), Space.Self);
        }



        if (GameLoop.MAIN.InputManager.HorizontalAxis.CurrentState == KS.State.Key_HeldDown)
        {
            //Move Right
            Debug.Log("Move Right");
            base._XForm_CameraRoot.Translate(new Vector3(Move_Speed * Time.deltaTime, 0f, 0f), Space.Self);
        }
        else if (GameLoop.MAIN.InputManager.HorizontalAxis.Negative_CurrentState == KS.State.Key_HeldDown)
        {
            //Move Left
            Debug.Log("Move Left");
            base._XForm_CameraRoot.Translate(new Vector3(-Move_Speed * Time.deltaTime, 0f, 0f), Space.Self);
        }





        if (GameLoop.MAIN.InputManager.MoveUpDown.CurrentState == KS.State.Key_HeldDown)
        {
            //Move Up
            Debug.Log("Move Up");
            base._XForm_CameraRoot.Translate(new Vector3(0f, Move_Speed * Time.deltaTime, 0f), Space.Self);
        }
        else if (GameLoop.MAIN.InputManager.MoveUpDown.Negative_CurrentState == KS.State.Key_HeldDown)
        {
            //Move Down
            Debug.Log("Move Down");
            base._XForm_CameraRoot.Translate(new Vector3(0f, -Move_Speed * Time.deltaTime, 0f), Space.Self);
        }





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
        }



        if (GameLoop.MAIN.InputManager.ZoomCamera.CurrentState == KS.State.Key_HeldDown)
        {
            //Zoom the Camera
            Debug.Log("Zoom the Camera");
        }
    }
}
