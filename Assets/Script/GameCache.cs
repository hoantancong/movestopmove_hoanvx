using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameCache
{
    private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();
    public static Character GetCharacterCollider(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider,collider.GetComponent<Character>());
        }
        return characters[collider];
    }
}
