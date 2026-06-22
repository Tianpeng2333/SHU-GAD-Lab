using UnityEngine;

public enum WwiseShoeType
{
    Highheel,
    Sneaker
}

[RequireComponent(typeof(AkGameObj))]
[DisallowMultipleComponent]
public class WwiseCharacterFoley : MonoBehaviour
{
    [Header("Wwise")]
    public AK.Wwise.Event foleyEvent = new AK.Wwise.Event();
    public string shoeSwitchGroup = "ShoeType";
    public string movementSwitchGroup = "MovementType";
    public string surfaceSwitchGroup = "SurfaceType";
    public WwiseShoeType shoeType = WwiseShoeType.Highheel;

    [Header("Surface Detection")]
    public Transform raycastOrigin;
    public LayerMask groundLayers = ~0;
    public float raycastDistance = 2.0f;
    public WwiseSurfaceType defaultSurface = WwiseSurfaceType.Dirt;
    public bool inferSurfaceFromMaterialName = true;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void PlayWalkFootstep()
    {
        Play("Walk", true);
    }

    public void PlayMoveFootstep()
    {
        Play("Move", true);
    }

    public void PlayJump()
    {
        Play("Jump", false);
    }

    public void PlayLanding()
    {
        Play("Land", true);
    }

    private void Play(string movementType, bool setSurface)
    {
        if (string.IsNullOrWhiteSpace(movementType))
        {
            return;
        }

        AkUnitySoundEngine.SetSwitch(shoeSwitchGroup, shoeType.ToString(), gameObject);
        AkUnitySoundEngine.SetSwitch(movementSwitchGroup, movementType, gameObject);

        if (setSurface)
        {
            AkUnitySoundEngine.SetSwitch(surfaceSwitchGroup, DetectSurface().ToString(), gameObject);
        }

        foleyEvent.Post(gameObject);
    }

    private WwiseSurfaceType DetectSurface()
    {
        Vector3 origin = raycastOrigin ? raycastOrigin.position : transform.position;
        if (characterController != null)
        {
            origin += Vector3.up * 0.2f;
        }

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, raycastDistance, groundLayers, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.TryGetComponent(out WwiseSurfaceTag surfaceTag))
            {
                return surfaceTag.surfaceType;
            }

            if (inferSurfaceFromMaterialName && TryInferSurface(hit.collider, out WwiseSurfaceType inferredSurface))
            {
                return inferredSurface;
            }
        }

        return defaultSurface;
    }

    private static bool TryInferSurface(Collider collider, out WwiseSurfaceType surface)
    {
        if (collider.sharedMaterial != null && TryParseSurface(collider.sharedMaterial.name, out surface))
        {
            return true;
        }

        Renderer renderer = collider.GetComponent<Renderer>();
        if (renderer != null && renderer.sharedMaterial != null && TryParseSurface(renderer.sharedMaterial.name, out surface))
        {
            return true;
        }

        surface = default;
        return false;
    }

    private static bool TryParseSurface(string name, out WwiseSurfaceType surface)
    {
        foreach (WwiseSurfaceType value in System.Enum.GetValues(typeof(WwiseSurfaceType)))
        {
            if (name.IndexOf(value.ToString(), System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                surface = value;
                return true;
            }
        }

        surface = default;
        return false;
    }
}
