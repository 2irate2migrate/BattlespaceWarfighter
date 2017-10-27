using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.GameLogic.Input
{
    /// <summary>
    /// Input Key State for a regular button.
    /// 
    /// Unity Axis States:
    /// 0 when the key isn't pressed
    /// 1 when the key is pressed
    /// </summary>
    public class InputKeyState
    {
        //==============================================================================
        //
        //                                    CONSTANTS
        //
        //==============================================================================



        public enum State : int
        {
            _DEFAULT_ = 0,
            Key_Neutral = _DEFAULT_,

            /// <summary>
            /// State active on every frame that this key is held down with the exception of the first and last frame of the life cycle
            /// </summary>
            Key_HeldDown = 1,
            
            /// <summary>
            /// State active on the frame that this key is pressed
            /// </summary>
            Key_Pressed = 2,

            /// <summary>
            /// State active on the frame this key is released
            /// </summary>
            Key_Up = 3,
        }





        //==============================================================================
        //
        //                                    PROPERTIES
        //
        //==============================================================================



        protected State _CurrentState = State._DEFAULT_;

        public State CurrentState { get { return this._CurrentState; } }



        /// <summary>
        /// Name of the axis to check for
        /// </summary>
        protected string _AxisName = "";



        protected bool _isPressed = false;

        /// <summary>
        /// Is the button being pressed on this frame?
        /// </summary>
        public bool isPressed { get { return this._isPressed; } }









        //==============================================================================
        //
        //                                    CONSTRUCTORS
        //
        //==============================================================================



        public InputKeyState(string inAxisName)
        {
            if (!string.IsNullOrWhiteSpace(inAxisName))
                this._AxisName = inAxisName;
            else
                Debug.LogError("Attempted to setup an InputKeyState for a key with no axis named defined.");
        }





        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        protected void _ChangeState(State inState)
        {
            this._CurrentState = inState;
        }



        public virtual void UpdateState()
        {
            if (string.IsNullOrWhiteSpace(this._AxisName))
                return;

            _isPressed = UnityEngine.Input.GetAxis(this._AxisName) != 0;

            if(_isPressed)
            {
                if (_CurrentState == State.Key_Neutral)
                {
                    _ChangeState(State.Key_Pressed);
                }
                else if(_CurrentState == State.Key_Pressed)
                {
                    _ChangeState(State.Key_HeldDown);
                }
                else if(_CurrentState == State.Key_Up)
                {
                    _ChangeState(State.Key_Pressed);
                }
            }
            else
            {
                if (_CurrentState == State.Key_HeldDown || _CurrentState == State.Key_Pressed)
                {
                    _ChangeState(State.Key_Up);
                }
                else if (_CurrentState == State.Key_Up)
                {
                    _ChangeState(State.Key_Neutral);
                }
            }
        }
    }
}
