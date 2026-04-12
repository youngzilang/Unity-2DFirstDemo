using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageFX : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float loseColorRate;

    public void SetUpAfterImage(Sprite _sprite, float _loseColorRate)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _sprite;
        loseColorRate = _loseColorRate;
    }

    private void Update()
    {
        float alpha = spriteRenderer.color.a - loseColorRate * Time.deltaTime;

        if (alpha <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Color newColor = spriteRenderer.color;
            newColor.a = alpha;
            spriteRenderer.color = newColor;
        }   
    }

}
