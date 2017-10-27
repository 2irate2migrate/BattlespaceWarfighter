using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    //==============================================================================
    //
    //                                    PROPERTIES
    //
    //==============================================================================



    public static GameLoop MAIN
    {
        get { return GameLoop._MAIN; }
    }

    private static GameLoop _MAIN = null;



    public Assets.GameLogic.Input.InputManager InputManager = null;






    //==============================================================================
    //
    //                                UNITY LIFECYCLE
    //
    //==============================================================================



    // Use this for initialization
    void Start () {
        Debug.Log("Hello, Space Universe!");

        _MAIN = this;

        if (InputManager == null)
        {
            this.GetComponent<Assets.GameLogic.Input.InputManager>();

            if (InputManager == null)
                Debug.LogError("Could not load an Input Manager for the project.");
        }
	}
	


	// Update is called once per frame
	void Update ()
    {
        //Update the state of all input keys the game is tracking.
        InputManager?.UpdateInputKeys();
	}
}
