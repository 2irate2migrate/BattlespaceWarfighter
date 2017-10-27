using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControllerManager : MonoBehaviour
{
    //==============================================================================
    //
    //                                    PROPERTIES
    //
    //==============================================================================



    protected _BaseCameraController _CurrentCameraController = null;
    
    /// <summary>
    /// The active camera controller assigned to this manager
    /// </summary>
    public _BaseCameraController CurrentCameraController
    {
        get { return this._CurrentCameraController; }
        set { this._CurrentCameraController = value; }
    }





    //==============================================================================
    //
    //                                UNITY LIFECYCLE
    //
    //==============================================================================



    // Use this for initialization
    void Start () {
        _CurrentCameraController = this.GetComponent<DefaultCameraController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
