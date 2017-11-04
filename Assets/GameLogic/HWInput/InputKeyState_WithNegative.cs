namespace Assets.GameLogic.HWInput
{
    public class InputKeyState_WithNegative : InputKeyState
    {
        //==============================================================================
        //
        //                                    PROPERTIES
        //
        //==============================================================================



        private State _Negative_CurrentState = State._DEFAULT_;

        public State Negative_CurrentState { get { return this._Negative_CurrentState; } }



        protected bool _Negative_isPressed = false;

        /// <summary>
        /// Is the button being pressed on this frame?
        /// </summary>
        public bool Negative_isPressed { get { return this._Negative_isPressed; } }





        //==============================================================================
        //
        //                                    CONSTRUCTORS
        //
        //==============================================================================



        public InputKeyState_WithNegative(string inAxisName) : base(inAxisName) { }





        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        protected void _Negative_ChangeState(State inState)
        {
            this._Negative_CurrentState = inState;
        }



        public override void UpdateState()
        {
            if (string.IsNullOrWhiteSpace(base._AxisName))
                return;

            #region Check for postive key

            _isPressed = UnityEngine.Input.GetAxis(this._AxisName) > 0;

            if (_isPressed)
            {
                if (_CurrentState == State.Key_Neutral)
                {
                    _ChangeState(State.Key_Pressed);
                }
                else if (_CurrentState == State.Key_Pressed)
                {
                    _ChangeState(State.Key_HeldDown);
                }
                else if (_CurrentState == State.Key_Up)
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

            #endregion


            #region Check Negative Key

            _Negative_isPressed = UnityEngine.Input.GetAxis(this._AxisName) < 0;

            if (_Negative_isPressed)
            {
                if (_Negative_CurrentState == State.Key_Neutral)
                {
                    _Negative_ChangeState(State.Key_Pressed);
                }
                else if (_Negative_CurrentState == State.Key_Pressed)
                {
                    _Negative_ChangeState(State.Key_HeldDown);
                }
                else if (_Negative_CurrentState == State.Key_Up)
                {
                    _Negative_ChangeState(State.Key_Pressed);
                }
            }
            else
            {
                if (_Negative_CurrentState == State.Key_HeldDown || _Negative_CurrentState == State.Key_Pressed)
                {
                    _Negative_ChangeState(State.Key_Up);
                }
                else if (_Negative_CurrentState == State.Key_Up)
                {
                    _Negative_ChangeState(State.Key_Neutral);
                }
            }

            #endregion
        }
    }
}
