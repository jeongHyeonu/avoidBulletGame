using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 10f;
    private Vector3 bulletDir;
    private Vector3 playerPos;

    private void OnEnable()
    {
        bulletSpeed = Random.Range(5f, 12f);
        playerPos = Player.Instance.transform.position;
        bulletDir = (playerPos - this.gameObject.transform.position).normalized;
        //this.gameObject.GetComponent<Rigidbody>().AddForce(PlayerMove.Instance.transform.position);
        Invoke("disableBullet", 5f);
    }

    private void Update()
    {
        if (this.gameObject.activeSelf == false) return;
        else
        {
            transform.position += bulletDir * bulletSpeed * Time.deltaTime;
        }
    }

    private void disableBullet()
    {
        this.gameObject.transform.SetAsLastSibling();
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character")
        {
            Player.Instance.ani.SetBool("Die", true);
            Player.Instance.isDied = true;
            Player.Instance.HIttedParticle.GetComponent<ParticleSystem>().Play();

            Invoke("GameOverByBullet", 2f);

        }
    }
    private void GameOverByBullet()
    {
        GameManager.Instance.GameOver();
    }
}
