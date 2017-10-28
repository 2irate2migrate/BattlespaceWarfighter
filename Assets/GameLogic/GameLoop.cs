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



    /// <summary>
    /// Reference to the main GameLoop singleton.  Lives through scene changes.
    /// </summary>
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
    void Start () {
        Debug.Log("Hello, Space Universe!");

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
