using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    public void OnThiefEntered()
    {
        _alarm.EnableAlarm();
    }

    public void OnThiefExited()
    {
        _alarm.DisableAlarm();
    }
}