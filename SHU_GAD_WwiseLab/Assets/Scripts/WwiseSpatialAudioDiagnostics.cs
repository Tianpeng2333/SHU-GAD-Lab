using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class WwiseSpatialAudioDiagnostics : MonoBehaviour
{
    private const string LogPrefix = "[Wwise Spatial]";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Bootstrap()
    {
        Install();
        SceneManager.sceneLoaded += (_, _) => Install();
    }

    private static void Install()
    {
        var rooms = FindObjectsOfType<AkRoom>();
        var portals = FindObjectsOfType<AkRoomPortal>();
        var listeners = FindObjectsOfType<AkSpatialAudioListener>();

        foreach (var room in rooms)
        {
            if (room.GetComponent<WwiseSpatialAudioRoomProbe>() == null)
            {
                room.gameObject.AddComponent<WwiseSpatialAudioRoomProbe>().Initialize(room);
            }
        }

        foreach (var portal in portals)
        {
            if (portal.GetComponent<WwiseSpatialAudioPortalProbe>() == null)
            {
                portal.gameObject.AddComponent<WwiseSpatialAudioPortalProbe>().Initialize(portal);
            }
        }

        foreach (var listener in listeners)
        {
            if (listener.GetComponent<WwiseSpatialAudioListenerProbe>() == null)
            {
                listener.gameObject.AddComponent<WwiseSpatialAudioListenerProbe>().Initialize(listener);
            }
        }

        Debug.Log($"{LogPrefix} Installed diagnostics. Rooms={rooms.Length}, Portals={portals.Length}, SpatialListeners={listeners.Length}, WwiseInitialized={AkUnitySoundEngine.IsInitialized()}");
    }
    public static void Log(string message)
    {
        Debug.Log($"{LogPrefix} {message}");
    }
}

[DisallowMultipleComponent]
public sealed class WwiseSpatialAudioRoomProbe : MonoBehaviour
{
    private AkRoom room;

    public void Initialize(AkRoom sourceRoom)
    {
        room = sourceRoom;
        var roomCollider = room.GetComponent<Collider>();
        WwiseSpatialAudioDiagnostics.Log($"Room '{room.name}' ready. Priority={room.priority}, Trigger={(roomCollider != null && roomCollider.isTrigger)}, Bounds={(roomCollider != null ? roomCollider.bounds.ToString() : "none")}");
    }

    private void OnTriggerEnter(Collider other)
    {
        var roomAwareObject = other.GetComponent<AkRoomAwareObject>();
        if (roomAwareObject != null)
        {
            WwiseSpatialAudioDiagnostics.Log($"Enter Room '{room.name}' <- '{other.name}' / AkRoomAwareObject='{roomAwareObject.name}'");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var roomAwareObject = other.GetComponent<AkRoomAwareObject>();
        if (roomAwareObject != null)
        {
            WwiseSpatialAudioDiagnostics.Log($"Exit Room '{room.name}' <- '{other.name}' / AkRoomAwareObject='{roomAwareObject.name}'");
        }
    }
}

[DisallowMultipleComponent]
public sealed class WwiseSpatialAudioPortalProbe : MonoBehaviour
{
    private AkRoomPortal portal;

    public void Initialize(AkRoomPortal sourcePortal)
    {
        portal = sourcePortal;
    }

    private void Start()
    {
        WwiseSpatialAudioDiagnostics.Log($"Portal '{portal.name}' ready. Active={portal.portalActive}, BackRoom='{RoomName(portal.GetRoom(0))}', FrontRoom='{RoomName(portal.GetRoom(1))}'");
    }

    private static string RoomName(AkRoom room)
    {
        return room != null ? room.name : "Outdoors/None";
    }
}

[DisallowMultipleComponent]
public sealed class WwiseSpatialAudioListenerProbe : MonoBehaviour
{
    private AkSpatialAudioListener listener;

    public void Initialize(AkSpatialAudioListener sourceListener)
    {
        listener = sourceListener;
    }

    private void Start()
    {
        var listenerCollider = listener.GetComponent<Collider>();
        var listenerRigidbody = listener.GetComponent<Rigidbody>();
        WwiseSpatialAudioDiagnostics.Log($"Listener '{listener.name}' ready. HasCollider={listenerCollider != null}, ColliderTrigger={(listenerCollider != null && listenerCollider.isTrigger)}, HasRigidbody={listenerRigidbody != null}, RigidbodyKinematic={(listenerRigidbody != null && listenerRigidbody.isKinematic)}");
    }
}
