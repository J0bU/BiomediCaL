using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodAnimationGame : MonoBehaviour
{
    [SerializeField] public Animator bloodEffectAnimation;
    [SerializeField] public Animator alarmEffectAnimation;
    [SerializeField] private string startAnimation = "BloodAlert";
    [SerializeField] private string startAlarmAnimation = "AlarmAnimation";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bloodEffectAnimation.Play(startAnimation, 0, 0.0f);
            alarmEffectAnimation.Play(startAlarmAnimation, 0, 0.0f);
        }
    }
}
