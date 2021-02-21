using UnityEngine;
using System.Collections;

public class ShowTheInput : MonoBehaviour {

    //public Sprite xbox360, xboxone, ps3, ps4, thrustmaster;
    
    public string nameOf360Glyph = "";
    public string nameOfXboxOneGlyph = "";
    public string nameOfPS3Glyph = "";
    public string nameOfPS4Glyph = "";
    public string nameOfThrustmasterGlyph = "";
    SpriteRenderer spr;
    private string nameOf2ndPath;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();

        HelperThingy("Xbox 360 Controller", nameOf360Glyph);
        HelperThingy("Sony DualShock 3", nameOfPS3Glyph);
        HelperThingy("Sony DualShock 4", nameOfPS4Glyph);
        HelperThingy("Xbox One Controller", nameOfXboxOneGlyph);
        HelperThingy("Thrustmaster USB Joystick", nameOfThrustmasterGlyph);
        HelperThingy("XInput Gamepad 1", nameOf360Glyph);
    }

    void HelperThingy(string nameOfController, string nameOfTheGlyph)
    {
        var activeController = ShowTheInputManager.Instance.activeController;
        if (activeController.name == nameOfController)
        {
            switch(nameOfController)
            {
                case "Xbox 360 Controller":
                    nameOf2ndPath = "xbox360";
                    spr.sprite = Resources.Load<Sprite>(nameOfTheGlyph);
                    print("360");
                    break;
                case "Xbox One Controller":
                    print("one");
                    nameOf2ndPath = "xboxone";
                    spr.sprite = Resources.Load<Sprite>(nameOfTheGlyph);
                    break;
                case "Sony DualShock 3":
                    print("ps3");
                    nameOf2ndPath = "PS3";
                    spr.sprite = Resources.Load<Sprite>(nameOfTheGlyph);
                    break;
                case "Sony DualShock 4":
                    print("ps4");
                    nameOf2ndPath = "PS3";
                    spr.sprite = Resources.Load<Sprite>(nameOfTheGlyph);
                    break;
                case "XInput Gamepad 1":
                    nameOf2ndPath = "xbox360";
                    spr.sprite = Resources.Load<Sprite>(nameOfTheGlyph);
                    break;
                default:
                    print("nothing was found");
                    break;
            }
        }
    }

}
