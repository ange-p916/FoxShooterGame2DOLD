using UnityEngine;
using System.Collections;
using System.IO;
using Rewired;

public class ShowTheInputManager : MonoBehaviour {

    public Controller activeController;
    public static ShowTheInputManager Instance;
    Player p;

    void Awake()
    {
        Instance = this;
        p = ReInput.players.GetPlayer(0);
        DoJoystickStuff();
       
    }
    void DoJoystickStuff()
    {
        activeController = p.controllers.Joysticks[0];
    }
}
