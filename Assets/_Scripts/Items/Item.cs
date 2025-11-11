using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Item : MonoBehaviour, ITriggerTarget
{
    [SerializeField] protected string itemName;
    [SerializeField] protected bool expiresInstantly;
    [SerializeField] protected string timeToExpire;

    protected AudioSource _audioSource;

    protected PlayerMovement player;

    protected virtual void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    protected virtual void ApplyItem()
    {
        if(expiresInstantly)
        {
            FinishApplyItem();
        }
    }

    protected virtual void FinishApplyItem()
    {
        Destroy(gameObject);
    }

    public void HitByPlayer(PlayerMovement player)
    {
        this.player = player;

        SoundManager.Instance.PlayFx(AudioFx.BallHit, _audioSource);

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = true;

        ApplyItem();
    }

}
