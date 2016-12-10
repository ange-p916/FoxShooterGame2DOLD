using UnityEngine;
using System.Collections;
using Rewired.Data.Mapping;
using Rewired;

public class ControllerGlyphs : MonoBehaviour {

    [SerializeField]
    private ControllerEntry[] controllers;

    private static ControllerGlyphs Instance;

    void Awake()
    {
        Instance = this; // set up a singleton
    }

    public static Sprite GetGlyph(System.Guid joystickGuid, int elementIdentifierId, AxisRange axisRange)
    {
        if (Instance == null) return null;
        if (Instance.controllers == null) return null;

        // Try to find the glyph
        for (int i = 0; i < Instance.controllers.Length; i++)
        {
            if (Instance.controllers[i] == null) continue;
            if (Instance.controllers[i].joystick == null) continue; // no joystick assigned
            if (Instance.controllers[i].joystick.Guid != joystickGuid) continue; // guid does not match
            return Instance.controllers[i].GetGlyph(elementIdentifierId, axisRange);
        }

        return null;
    }

    [System.Serializable]
    private class ControllerEntry
    {
        public string name;
        public HardwareJoystickMap joystick;
        public GlyphEntry[] glyphs;

        public Sprite GetGlyph(int elementIdentifierId, AxisRange axisRange)
        {
            if (glyphs == null) return null;
            for (int i = 0; i < glyphs.Length; i++)
            {
                if (glyphs[i] == null) continue;
                if (glyphs[i].elementIdentifierId != elementIdentifierId) continue;
                return glyphs[i].GetGlyph(axisRange);
            }
            return null;
        }
    }

    [System.Serializable]
    private class GlyphEntry
    {
        public int elementIdentifierId;
        public Sprite glyph;
        public Sprite glyphPos;
        public Sprite glyphNeg;

        public Sprite GetGlyph(AxisRange axisRange)
        {
            switch (axisRange)
            {
                case AxisRange.Full: return glyph;
                case AxisRange.Positive: return glyphPos != null ? glyphPos : glyph;
                case AxisRange.Negative: return glyphNeg != null ? glyphNeg : glyph;
            }
            return null;
        }
    }
}