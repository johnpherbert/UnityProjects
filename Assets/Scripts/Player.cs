using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float HP = 200f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;

    [Header("Player Movement")]
    [SerializeField] float horMoveSpeed = 10f;
    [SerializeField] float verMoveSpeed = 10f;
    [SerializeField] float padding = 1f;

    [Header("Projectile")]
    [SerializeField] GameObject lazerPrefab;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.7f;


    // Start is called before the first frame update

    Coroutine firingCoroutine;
    float xmin;
    float xmax;
    float ymin;
    float ymax;

    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        FireContinuously();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * horMoveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * verMoveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xmin, xmax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, ymin, ymax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject lazer = Instantiate(lazerPrefab, transform.position, Quaternion.identity) as GameObject;
            lazer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xmin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xmax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        ymin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        ymax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        HP -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
        Destroy(gameObject);
    }

    public float GetHP()
    {
        return HP;
    }
}
