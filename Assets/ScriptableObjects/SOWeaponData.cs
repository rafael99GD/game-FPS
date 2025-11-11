using UnityEngine;


[CreateAssetMenu(fileName = "Weapon Data", menuName = "ScriptableObjects/Weapon Data", order = 0)]
public class SOWeaponData : ScriptableObject
{
    [Header("Weapon Data")]
    public float m_damage;
    public float m_forceToApply;
    public float m_weaponRange;
    public int m_maxAmmo;
    public int m_maxBulletsPerShoot;
    public int m_bulletsPerSecond;


    [Header("Weapon Accuracy")]
    public float m_accuracyDropPerShot;
    public float m_accuracyRecoveryPerSecond;

    [Header("Weapon Type")]
    public bool m_isAMachineGun;
    public bool m_isARocketLauncher;

    [Header("Weapon Customs")]
    public Texture2D m_crosshairTexture;


}
