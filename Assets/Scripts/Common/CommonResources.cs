using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonResources {
    
    public static Sprite MissingSprite => _missingSprite ? _missingSprite : _missingSprite = LoadSprite("missing");
    private static Sprite _missingSprite;
    
    private static Sprite LoadSprite(string name) => Resources.Load<Sprite>(name);
}
