using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cannon : MonoBehaviour
{
    [SerializeField] public GameObject BulletPool;
    [SerializeField] public GameObject ShootParticle;

    // Start is called before the first frame update
    public void ShootBullet(float _cooltime)
    {
        GameObject targetObj = null;
        for(int i = 0; i < BulletPool.transform.childCount; i++)
        {
            targetObj = BulletPool.transform.GetChild(i).gameObject;
            if (targetObj.activeSelf==false) break; // 비활성화된 총알 찾을때까지 반복
        }
        targetObj.transform.position = this.transform.position; // 총알 위치를 Cannon 위치로

        // 캐논 머리 플레이어방향으로, 머리 커졌다가 작아짐
        this.transform.GetChild(1).DOScale(1.2f, 0.5f).From(1.0f)
            .OnComplete( () => this.transform.GetChild(1).DOScale(1.0f, 1.0f) );
        this.transform.GetChild(1).LookAt(Player.Instance.transform); 

        targetObj.SetActive(true); // 총알 활성화
        ShootParticle.GetComponent<ParticleSystem>().Play(); // 파티클
    }



}
