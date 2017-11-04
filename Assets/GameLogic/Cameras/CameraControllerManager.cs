using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.GameLogic.Cameras
{
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



        public Camera Camera_Current {
            get {
                if (_CurrentCameraController == null)
                    return null;

                return _CurrentCameraController.Camera;
            }
        }





        //==============================================================================
        //
        //                                UNITY LIFECYCLE
        //
        //==============================================================================



        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}