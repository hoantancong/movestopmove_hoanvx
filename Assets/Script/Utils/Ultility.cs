
using UnityEngine;

public class Ultility
{
    public static bool GetRandom5050()
    {
        return Random.Range(0, 20) < 5;
    }
}
