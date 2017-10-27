using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.GameLogic.Input
{
    [RequireComponent(typeof(GameLoop))]
    public class InputManager : MonoBehaviour
    {
        //==============================================================================
        //
        //                                    PROPERTIES
        //
        //==============================================================================



        protected List<InputKeyState> _AllInputKeys = new List<InputKeyState>();



        public InputKeyState_WithNegative VerticalAxis = new InputKeyState_WithNegative("Vertical");



        public InputKeyState_WithNegative HorizontalAxis = new InputKeyState_WithNegative("Horizontal");



        public InputKeyState_WithNegative MoveUpDown = new InputKeyState_WithNegative("MoveUpDown");



        public InputKeyState_WithNegative CameraRoll = new InputKeyState_WithNegative("CameraRoll");



        public InputKeyState ResetCameraRotation = new InputKeyState("ResetCameraRotation");



        public InputKeyState ZoomCamera = new InputKeyState("ZoomCamera");





        //==============================================================================
        //
        //                                UNITY LIFECYCLE
        //
        //==============================================================================



        public void Start()
        {
            _AllInputKeys.Add(this.VerticalAxis);
            _AllInputKeys.Add(this.HorizontalAxis);
            _AllInputKeys.Add(this.MoveUpDown);
            _AllInputKeys.Add(this.CameraRoll);
            _AllInputKeys.Add(this.ResetCameraRotation);
            _AllInputKeys.Add(this.ZoomCamera);
        }





        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        /// <summary>
        /// Update the state of all input keys in our game that we are tracking
        /// </summary>
        public void UpdateInputKeys()
        {
            if (this._AllInputKeys == null || this._AllInputKeys.Count < 1)
                return;

            for(int i=0;i<this._AllInputKeys.Count;i++)
                this._AllInputKeys[i].UpdateState();
        }
    }
}