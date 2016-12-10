using UnityEngine;
using System.Collections;
using Rewired;

public class GlyphManager : MonoBehaviour {

    ControllerStuff controllerStuff;

	void GetGlyphsFromIDs(ActionElementMap aem)
    {
        controllerStuff = new ControllerStuff();
        for (int i = 0; i < controllerStuff.glyphs.Count; i++)
        {
            //if glyph image doesnt fit the action
            if(controllerStuff.glyphs[i]._elementIdentifierID != aem.elementIdentifierId)
            {

            }
        }

    }
}
