using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprite;

    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        frame++;
        if(frame >= sprite.Length) {
            frame = 0;
        }

        if(frame >= 0 && frame < sprite.Length) {
            spriteRenderer.sprite = sprite[frame];
        }

        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
        
    }
}
