using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameLogic.Cameras
{
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
        public Camera Camera
        {
            get { return this._Camera; }
        }

        protected Camera _Camera = null;



        protected Transform _XForm_CameraRoot = null;

        public Transform XForm_CameraRoot
        {
            get { return this._XForm_CameraRoot; }
        }



        /// <summary>
        /// The Current velocity of the camera
        /// </summary>
        public Vector3 Velocity_Current = Vector3.zero;



        /// <summary>
        /// Maximum speed that the camera can travel in Meters per second
        /// </summary>
        [Range(0.01f, 500f)]
        public float Velocity_Max = 10f;




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





        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        public abstract void UpdateCamera();
    }
}