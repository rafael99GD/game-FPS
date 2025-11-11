using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerySimplePistol : MonoBehaviour
{
	[Header("Weapon Data")]
	[SerializeField] private SOWeaponData soWeaponData;
	[Space]

	private  Transform m_raycastSpot;
    private float     m_damage        = 80.0f;
    private float     m_forceToApply  = 20.0f;
    private float     m_weaponRange   = 9999.0f;
    private Texture2D m_crosshairTexture;
    //public  AudioClip m_fireSound;
    private bool      m_canShot = true;

    private int       m_maxAmmo;
    private int       m_currentAmmo;

    private int       m_maxBulletsPerShoot;
    private bool      m_isAMachineGun = false;

	public  int       m_bulletsPerSecond;
    private float     m_timeBetweenShots;
    private float     m_shotTimer = 0;

    private bool       m_isARocketLauncher = false;

    private float     m_accuracyDropPerShot;
    private float     m_accuracyRecoveryPerSecond;
	private float     m_currentAccuracy;

	private AudioSource _audioSource;

	private void Start()
    {
		GetWeaponData();
		m_currentAmmo = m_maxAmmo;
		m_shotTimer   = 0;
		m_timeBetweenShots = 1f / m_bulletsPerSecond;
		_audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		m_shotTimer += Time.deltaTime;

		m_currentAccuracy += Time.deltaTime * m_accuracyRecoveryPerSecond;

		if (Input.GetButtonDown("Fire2"))
        {
			m_currentAmmo = m_maxAmmo;
		}

		m_canShot = (m_isAMachineGun) ? true : m_canShot;

		if (m_shotTimer >= m_timeBetweenShots && m_canShot)
		{
			if (Input.GetButton("Fire1"))
			{
				m_shotTimer = 0;

				if (m_isARocketLauncher)
                {
					ShootRocket();
                }
                else
                {
					Shot();
				}

				
			}
		}
		else if (Input.GetButtonUp("Fire1"))
        { 
			m_canShot = true;
        }
	}

    private void ShootRocket()
    {
        
    }

    private void OnGUI()
	{
		Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
		Rect auxRect = new Rect(center.x - 20, center.y - 20, 20, 20);
		GUI.DrawTexture(auxRect, m_crosshairTexture, ScaleMode.StretchToFill);
	}

	private void Shot()
	{
		if (m_currentAmmo <= 0)
        {
			return;
        }

		m_currentAmmo--;

		m_canShot = false;

        for (int i = 0; i < m_maxBulletsPerShoot; i++)
        {
			float   accuracyModifier  = (100 - m_currentAccuracy) / 1000;
			Vector3 directionForward  = m_raycastSpot.forward;
			directionForward.x       += UnityEngine.Random.Range(-accuracyModifier, accuracyModifier);
			directionForward.y       += UnityEngine.Random.Range(-accuracyModifier, accuracyModifier);
			directionForward.z       += UnityEngine.Random.Range(-accuracyModifier, accuracyModifier);
			m_currentAccuracy        -= m_accuracyDropPerShot;
			m_currentAccuracy         = Mathf.Clamp(m_currentAccuracy, 0, 100);

			Ray ray = new Ray(m_raycastSpot.position, directionForward);
			Debug.DrawRay(m_raycastSpot.position, directionForward, Color.green, 4);

			RaycastHit[] hits;
			hits = Physics.RaycastAll(m_raycastSpot.position, directionForward, 100.0F);
			float maxDistance = float.MaxValue;
			int   nearGObjectIndex = 0;

			for (int j = 0; j < hits.Length; j++)
			{
				RaycastHit aux = hits[j];
				float distance = Vector3.Distance(transform.position, aux.point);

				if (distance < maxDistance)
                {
					maxDistance = distance;
					nearGObjectIndex = j;
                }
			}

			if (hits.Length != 0)
            {
				if (hits[nearGObjectIndex].rigidbody != null)
                {
					hits[nearGObjectIndex].rigidbody.AddForce(ray.direction * m_forceToApply);
				}
				
				Debug.Log("Hit " + hits[nearGObjectIndex].transform.name);
			}
			
			
		}

		SoundManager.Instance.PlayFx(AudioFx.PistolShot, _audioSource);
	}

	private void GetWeaponData()
	{
		 m_damage = soWeaponData.m_damage;
		 m_forceToApply = soWeaponData.m_forceToApply;
		 m_weaponRange = soWeaponData.m_weaponRange;
		 m_maxAmmo = soWeaponData.m_maxAmmo;
		 m_maxBulletsPerShoot = soWeaponData.m_maxBulletsPerShoot;
		 m_bulletsPerSecond = soWeaponData.m_bulletsPerSecond;


		 m_accuracyDropPerShot = soWeaponData.m_accuracyDropPerShot;
		 m_accuracyRecoveryPerSecond = soWeaponData.m_accuracyRecoveryPerSecond;

		m_isAMachineGun = soWeaponData.m_isAMachineGun;
		m_isARocketLauncher = soWeaponData.m_isARocketLauncher;

		m_crosshairTexture = soWeaponData.m_crosshairTexture;
	}
}
