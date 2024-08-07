using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "enemigos", menuName = "enemy")]
public class enemigos : ScriptableObject
{
    #region variables
    public string name;
    public float damage;
    public float speed;
    public float firerate;
    public float bulletSpeed;
    public float maxHP;

    #endregion
    #region gets
    public string Name { get { return name; } }
    public float Damage { get { return damage; } }
    public float Speed { get { return speed; } }
    public float Firerate { get {  return firerate; } }
    public float BulletSpeed { get {  return bulletSpeed; } }

    #endregion
    
}
