using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private DoorTrigger _doorTrigger;
    [SerializeField] private AlarmTrigger _alarmTrigger;
    [SerializeField] private Door _door;
    [SerializeField] private Alarm _alarm;

    private void OnEnable()
    {
        _doorTrigger.ThiefFound += OnDoorTriggerFound;
        _doorTrigger.ThiefLost += OnDoorTriggerLost;

        _alarmTrigger.ThiefFound += OnAlarmTriggerFound;
        _alarmTrigger.ThiefLost += OnAlarmTriggerLost;
    }

    private void OnDisable()
    {
        _doorTrigger.ThiefFound -= OnDoorTriggerFound;
        _doorTrigger.ThiefLost -= OnDoorTriggerLost;

        _alarmTrigger.ThiefFound -= OnAlarmTriggerFound;
        _alarmTrigger.ThiefLost -= OnAlarmTriggerLost;
    }

    private void OnDoorTriggerFound()
    {
        _door.Open();
    }

    private void OnDoorTriggerLost()
    {
        _door.Close();
    }

    private void OnAlarmTriggerFound()
    {
        _alarm.EnableAlarm();
    }

    private void OnAlarmTriggerLost()
    {
        _alarm.DisableAlarm();
    }
}