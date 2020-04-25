using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string Name;
    public string Description;
    public Sprite CurrentWeaponSprite;
    public Color Color;
    public GameObject Animation;
    public GameObject IdleSprite;
    public Texture2D cursor;

    public abstract void Attack(PlayerBacteria player);
    public abstract IEnumerator AttackCheck(PlayerBacteria player, KeyCode keycode);
}
