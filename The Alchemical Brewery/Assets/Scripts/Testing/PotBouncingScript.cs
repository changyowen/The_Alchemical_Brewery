using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotBouncingScript : MonoBehaviour
{
    public Transform sprite_Transform;

    public Vector3 scale_beforeBounce;
    public Vector3 scale_afterBounce;
    public float lerpTime = 0.2f;
    public bool shaking = false;

    public IEnumerator StartShaking()
    {
        StopAllCoroutines();
        sprite_Transform.localScale = scale_beforeBounce;

        bool _shrinking = false;
        
        while(shaking)
        {
            if (!_shrinking)
            {
                yield return StartCoroutine(Transforming(_shrinking));
                Debug.Log("YESS");
                sprite_Transform.localScale = scale_afterBounce;
                _shrinking = true;
            }
            else
            {
                yield return StartCoroutine(Transforming(_shrinking));
                sprite_Transform.localScale = scale_beforeBounce;
                _shrinking = false;
            }
            yield return null;
        }

        sprite_Transform.localScale = scale_beforeBounce;
    }

    IEnumerator Transforming(bool _shrinking)
    {
        if(!_shrinking)
        {
            float _tempTimer = 0;
            while (_tempTimer < lerpTime)
            {
                Vector3 newScale = Vector3.Lerp(scale_beforeBounce, scale_afterBounce, _tempTimer / lerpTime);
                sprite_Transform.localScale = newScale;
                _tempTimer += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            float _tempTimer = 0;
            while (_tempTimer < lerpTime)
            {
                Vector3 newScale = Vector3.Lerp(scale_afterBounce, scale_beforeBounce, _tempTimer / lerpTime);
                sprite_Transform.localScale = newScale;
                _tempTimer += Time.deltaTime;
                yield return null;
            }
        }
    }
}
