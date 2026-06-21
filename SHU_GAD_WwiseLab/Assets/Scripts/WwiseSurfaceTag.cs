using UnityEngine;

public enum WwiseSurfaceType
{
    Dirt,
    Grass,
    Rock,
    Wood
}

[DisallowMultipleComponent]
public class WwiseSurfaceTag : MonoBehaviour
{
    public WwiseSurfaceType surfaceType = WwiseSurfaceType.Dirt;
}
