using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _BaseCameraController : MonoBehaviour
{
    //==============================================================================
    //
    //                                    PROPERTIES
    //
    //==============================================================================


    /// <summary>
    /// Reference to the camera that this controller is managing
    /// </summary>
    public Camera Camera {
        get { return this._Camera; }
    }

    protected Camera _Camera = null;



    protected Transform _XForm_CameraRoot = null;

    public Transform XForm_CameraRoot
    {
        get { return this._XForm_CameraRoot; }
    }




    //==============================================================================
    //
    //                                UNITY LIFECYCLE
    //
    //==============================================================================



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
