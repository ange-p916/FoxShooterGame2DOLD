using UnityEngine;
using System.Collections;
using Rewired;
using UnityEngine.UI;

public class ShowControllerGlyphs : MonoBehaviour {

    public Sprite shootImg, jumpImg, runImg, moveHorImg, moveLeftImg, moveRightImg, interactImg;

    Player p;

    void Start()
    {
        p = ReInput.players.GetPlayer(0);

        ShowGlyphs();
    }


	void ShowGlyphs()
    {
        Controller activeController = p.controllers.GetLastActiveController();
        if(activeController == null)
        {
            if(p.controllers.joystickCount > 0)
            {
                activeController = p.controllers.Joysticks[0];
            }
            else
            {
                activeController = p.controllers.Keyboard;
            }
        }

        if (activeController.type != ControllerType.Joystick) return;

        int drawCount = 0;

        for (int i = 0; i < ReInput.mapping.Actions.Count; i++)
        {
            ShowActiveActionSource(p, activeController, ReInput.mapping.Actions[i], ref drawCount);
        }

    }

    void ShowActiveActionSource(Player p, Controller controller, InputAction action, ref int count)
    {
        Joystick joystick = controller as Joystick;
        if (joystick == null) return;

        float value = p.GetAxis(action.id);
        if (value == 0f) return;

        if (!p.IsCurrentInputSource(action.id, controller.type, controller.id)) return; // not a source of this action

        // Get the sources contributing to this Action
        var sources = p.GetCurrentInputSources(action.id);
        ActionElementMap aem = null;

        // Find the first source on this controller for the Action
        for (int i = 0; i < sources.Count; i++)
        {
            if (sources[i].controller != controller) continue;
            aem = sources[i].actionElementMap;
            break; // only show one source for now
        }
        if (aem == null) return;

        // Find the glyph for the element on the controller
        Sprite glyph = ControllerGlyphs.GetGlyph(joystick.hardwareTypeGuid, aem.elementIdentifierId, aem.axisRange);
        if (glyph == null) return; // no glyph found

        // Draw the glyph to the screen
        //Rect rect = new Rect(count * 120, 30, glyph.textureRect.width, glyph.textureRect.height);
        //GUI.Label(new Rect(rect.x, rect.y + rect.height + 20, rect.width, rect.height), action.descriptiveName);
        //GUI.DrawTexture(rect, glyph.texture);

        count++;
    }

}
