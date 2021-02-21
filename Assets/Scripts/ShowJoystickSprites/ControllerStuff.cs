using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rewired.Data.Mapping;
using Rewired;

[System.Serializable]
public class ControllerStuff {

    public List<GlyphStuff> glyphs;

    //first i add all the glyphs to a file together with an id.
    //then get the action id and retrieve the glyph based on what action it is.
    public ControllerStuff()
    {
        glyphs = new List<GlyphStuff>();

        glyphs.Add(new GlyphStuff(0, "360_A"));
        glyphs.Add(new GlyphStuff(1, "360_B"));
        glyphs.Add(new GlyphStuff(2, "360_X"));
        glyphs.Add(new GlyphStuff(3, "360_Y"));
        glyphs.Add(new GlyphStuff(4, "360_Back"));
        glyphs.Add(new GlyphStuff(5, "360_Back_Alt"));
        glyphs.Add(new GlyphStuff(6, "360_Dpad"));
        glyphs.Add(new GlyphStuff(7, "360_Dpad_Down"));
        glyphs.Add(new GlyphStuff(8, "360_Dpad_Left"));
        glyphs.Add(new GlyphStuff(9, "360_Dpad_Right"));
        glyphs.Add(new GlyphStuff(10, "360_Dpad_Up"));
        glyphs.Add(new GlyphStuff(11, "360_LB"));
        glyphs.Add(new GlyphStuff(12, "360_RB"));
        glyphs.Add(new GlyphStuff(13, "360_LT"));
        glyphs.Add(new GlyphStuff(14, "360_RT"));
        glyphs.Add(new GlyphStuff(15, "360_Start"));
        glyphs.Add(new GlyphStuff(16, "360_Start_Alt"));
        glyphs.Add(new GlyphStuff(17, "360_Left_Stick"));
        glyphs.Add(new GlyphStuff(18, "360_Right_Stick"));
    }
}
