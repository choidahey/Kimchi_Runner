using UnityEngine;

public class HP : MonoBehaviour
{
    public PlayerController playerController;
    public Sprite OnHeart;
    public Sprite OffHeart;

    public SpriteRenderer spriteRenderer;
    public int HpNumber;

    void Update()
    {
        if (GameManager.Instance.hp >= HpNumber)
            spriteRenderer.sprite = OnHeart;
        else
            spriteRenderer.sprite = OffHeart;
    }
}
