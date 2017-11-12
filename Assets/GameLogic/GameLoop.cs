using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.GameLogic
{
    public class GameLoop : MonoBehaviour
    {
        //==============================================================================
        //
        //                                    PROPERTIES
        //
        //==============================================================================



        /// <summary>
        /// Reference to the main GameLoop singleton.  Lives through scene changes.
        /// </summary>
        public static GameLoop MAIN
        {
            get { return GameLoop._MAIN; }
        }

        private static GameLoop _MAIN = null;



        /// <summary>
        /// This manages the hardware inputs from Keyboard, Mouse, and Game Controller.
        /// </summary>
        public HWInput.InputManager InputManager = null;



        /// <summary>
        /// This dictates which camera is being actively rendered, and what type of controls and movement it should use.
        /// </summary>
        public Cameras.CameraControllerManager CameraManager = null;



        /// <summary>
        /// The Starfield following the main camera.
        /// </summary>
        protected Effects.InfiniteStarfield _InfiniteStarfield = null;




        public Data.DataManager DataManager = null;





        //==============================================================================
        //
        //                                UNITY LIFECYCLE
        //
        //==============================================================================



        /// <summary>
        /// Called before start
        /// </summary>
        void Awake()
        {
            if (_MAIN == null)
            {
                //Not destroyed when a new scene loads
                DontDestroyOnLoad(this.gameObject);
                //ToDo: Don't destroy GUI and Player Data
                _MAIN = this;
            }
            else if (_MAIN != this)
            {
                //Destroy this object, but persist the old one
                Destroy(this.gameObject);
            }
        }



        // Use this for initialization
        void Start()
        {
            Debug.Log("Hello, Space Universe!");

            if (InputManager == null)
            {
                this.GetComponent<Assets.GameLogic.HWInput.InputManager>();

                if (InputManager == null)
                    Debug.LogError("Could not load an Input Manager for the project.");
            }

            if(CameraManager == null)
            {
                CameraManager = GameObject.Find("Main Camera")?.GetComponent<Cameras.CameraControllerManager>();
                CameraManager.CurrentCameraController = GameObject.Find("Main Camera")?.GetComponent<Cameras.DefaultCameraController>();

                if (CameraManager == null)
                    Debug.Log($"Cannot find {typeof(Cameras.CameraControllerManager).Name} component on the GameObject:'Main Camera'.");
                else
                {
                    var Controller = CameraManager.CurrentCameraController as Cameras.DefaultCameraController;
                    Controller.TestInit();
                }
            }

            if (_InfiniteStarfield == null)
            {
                _InfiniteStarfield = GameObject.Find("Star Field").GetComponent<Effects.InfiniteStarfield>();
                _InfiniteStarfield.TestInit();
            }

            if(DataManager == null)
            {
                DataManager = new Data.DataManager();
            }

            //Load the default mod
            Modding.ModParser.LoadDefaultMod();
        }



        void Update()
        {
            //Update the state of all input keys the game is tracking.
            InputManager?.UpdateInputKeys();

            if(CameraManager && CameraManager.CurrentCameraController)
                CameraManager.CurrentCameraController.UpdateCamera();

            if (_InfiniteStarfield)
                _InfiniteStarfield.UpdateStarfield();
        }
    }
}
