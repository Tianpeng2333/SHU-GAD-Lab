using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(AkRoomAwareObject))]
public sealed class OutdoorAmbienceIndoorRtpcDriver : MonoBehaviour
{
    [SerializeField] private string rtpcName = "RTPC_PlayerIndoor";
    [SerializeField] private int indoorPriorityThreshold = 1;
    [SerializeField] private float outdoorValue = 0f;
    [SerializeField] private float indoorValue = 1f;
    [SerializeField] private float fadeSpeed = 4f;
    [SerializeField] private int wwiseInterpolationMs = 100;

    private readonly List<AkRoom> rooms = new List<AkRoom>();
    private float currentValue;
    private float targetValue;

    private void Start()
    {
        currentValue = outdoorValue;
        targetValue = outdoorValue;
        SetRtpc(currentValue, 0);
    }

    private void Update()
    {
        targetValue = IsInIndoorRoom() ? indoorValue : outdoorValue;
        currentValue = Mathf.MoveTowards(currentValue, targetValue, fadeSpeed * Time.deltaTime);
        SetRtpc(currentValue, wwiseInterpolationMs);
    }

    private void OnTriggerEnter(Collider other)
    {
        var room = other.GetComponent<AkRoom>();
        if (room != null && !rooms.Contains(room))
        {
            rooms.Add(room);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var room = other.GetComponent<AkRoom>();
        if (room != null)
        {
            rooms.Remove(room);
        }
    }

    private bool IsInIndoorRoom()
    {
        var highestPriority = int.MinValue;

        for (var i = rooms.Count - 1; i >= 0; i--)
        {
            var room = rooms[i];
            if (room == null || !room.isActiveAndEnabled)
            {
                rooms.RemoveAt(i);
                continue;
            }

            if (room.priority > highestPriority)
            {
                highestPriority = room.priority;
            }
        }

        return highestPriority >= indoorPriorityThreshold;
    }

    private void SetRtpc(float value, int interpolationMs)
    {
        if (!AkUnitySoundEngine.IsInitialized() || string.IsNullOrWhiteSpace(rtpcName))
        {
            return;
        }

        AkUnitySoundEngine.SetRTPCValue(rtpcName, value, AkUnitySoundEngine.AK_INVALID_GAME_OBJECT, interpolationMs);
    }
}
