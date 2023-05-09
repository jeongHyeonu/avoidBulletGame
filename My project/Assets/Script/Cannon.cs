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
            if (targetObj.activeSelf==false) break; // ��Ȱ��ȭ�� �Ѿ� ã�������� �ݺ�
        }
        targetObj.transform.position = this.transform.position; // �Ѿ� ��ġ�� Cannon ��ġ��

        // ĳ�� �Ӹ� �÷��̾��������, �Ӹ� Ŀ���ٰ� �۾���
        this.transform.GetChild(1).DOScale(1.2f, 0.5f).From(1.0f)
            .OnComplete( () => this.transform.GetChild(1).DOScale(1.0f, 1.0f) );
        this.transform.GetChild(1).LookAt(Player.Instance.transform); 

        targetObj.SetActive(true); // �Ѿ� Ȱ��ȭ
        ShootParticle.GetComponent<ParticleSystem>().Play(); // ��ƼŬ
    }



}
