using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder;

    [SerializeField] private float speed = 4f;
    [SerializeField] private float rotationSpeed = 1f;
    private float currentSpeed;
    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value;}
    }

    private Weapon weapon;
    public Weapon Weapon
    {
        get { return weapon; }
        set
        {
            if (weapon != null) Destroy(weapon.gameObject);
            weapon = value;
            weapon.transform.parent = weaponHolder;
            weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
        }
    }

    public bool MustDie { get; set; }
    private bool isInvincible;
    public bool IsInvincible
    {
        get { return isInvincible; }
        set 
        { 
            isInvincible = value;
            if(!isInvincible && MustDie) Death();
        }
    }
    private Rigidbody rb;
    private Coroutine rotateCoroutine;
    private bool canShoot = false;
    void Start()
    {
        currentSpeed = speed;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartRotate();
        }
        if (Input.GetMouseButton(0) && !Input.GetMouseButtonUp(0) && !Input.GetMouseButtonDown(0))
        {
            if (canShoot)
            {
                Rotate();
                weapon?.Shoot();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            canShoot = false;
        }
    }
    void StartRotate()
    {
        if (rotateCoroutine != null) StopCoroutine(rotateCoroutine);
        rotateCoroutine = StartCoroutine(RotateToShoot());
    }
    IEnumerator RotateToShoot()
    {
        float angle = CalculateAngle();
        float t = 0;
        while (t < 1f)
        {
            Rotate();
            t += Time.deltaTime * rotationSpeed/3f;
            yield return new WaitForEndOfFrame();
        }
        canShoot = true;
    }
    void Rotate()
    {
        float angle = CalculateAngle();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, -angle, 0f), Time.deltaTime * rotationSpeed);
    }
    public void Death()
    {
        GameManager.instance.EndGame();
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        Vector3 movement = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.MovePosition(rb.position + currentSpeed * Time.fixedDeltaTime * movement);
    }
    float CalculateAngle()
    {
        Vector3 mPos = Input.mousePosition;
        Vector3 bPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 delta = mPos - bPos;
        return Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - 90f;
    }
}
