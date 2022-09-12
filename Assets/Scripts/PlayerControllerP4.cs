using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerP4 : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody playerRg;
    private GameObject focalPoint;
    public bool hasPowerUp = false;
    private float powerupStrength = 10f;
    public GameObject powerupIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRg = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRg.AddForce(focalPoint.transform.forward * speed  * forwardInput);
        powerupIndicator.gameObject.transform.position = transform.position + new Vector3(0, -0.5f,0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerUp = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Debug.Log("Collided with: " +collision.gameObject.name + " with power up set to "+hasPowerUp);
            Vector3 awayFromPlayer = enemyRigidBody.transform.position - transform.position;
            enemyRigidBody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerupIndicator.gameObject.SetActive(false);
    }
}
