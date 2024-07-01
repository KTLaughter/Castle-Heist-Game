using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraBehavior : MonoBehaviour
{
    public float bulletRate;
    private bool allowShoot = true;
    public float xSensitivity;
    public float ySensitivity;
    public Transform orientation;
    float xRotation;
    float yRotation;

    public GameObject bullet;
    public float bulletSpeed = 100f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float rotateVertical = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySensitivity;
        float rotateHorizontal = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;

        yRotation += rotateHorizontal;
        xRotation-= rotateVertical;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        if (Input.GetMouseButtonDown(0) && (allowShoot))
        {
            StartCoroutine(Wait());
        }
    }
    
    IEnumerator Wait()
    {
        allowShoot = false;
        GameObject newBullet = Instantiate(bullet) as GameObject;
        newBullet.transform.position = transform.position + Camera.main.transform.forward * 2;
        Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
        bulletRB.velocity = Camera.main.transform.forward * bulletSpeed;
        yield return new WaitForSeconds(bulletRate);
        allowShoot = true;
        
    }
    
}

