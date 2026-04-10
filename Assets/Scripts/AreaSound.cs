using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSound : MonoBehaviour
{
    [SerializeField] private int index;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (AudioManager.instance == null) return;

        if (collision.TryGetComponent(out Player player))
        {
            AudioManager.instance.PlaySFX(index);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (AudioManager.instance == null) return;

        if (collision.TryGetComponent(out Player player))
        {
            AudioManager.instance.StopSFXWithTime(index);
        }
    }
}
