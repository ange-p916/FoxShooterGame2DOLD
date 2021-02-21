using UnityEngine;
using System.Collections;

[System.Serializable]
public class GlyphStuff {
    public int _elementIdentifierID;
    public string _glyphName;
	
    public GlyphStuff(int elementIdentifierID, string glyphName)
    {
        _elementIdentifierID = elementIdentifierID;
        _glyphName = glyphName;
    }
}
