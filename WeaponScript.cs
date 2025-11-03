using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private Rigidbody rigidbody;
    private float momentum;
    private Vector3 prevPosition;
    private Vector3 velocity;
    private int recoveryStage;
    [SerializeField] private AnimationCurve damageCurve;
    [SerializeField] private AudioSource hitAudio;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        recoveryStage = PlayerPrefs.GetInt("RecoveryStage", 2);
    }

    // Update is called once per frame
    void Update()
    {
        var dif = (transform.position - prevPosition);
        velocity = dif / Time.deltaTime;

        prevPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitGO = collision.gameObject;

        Vector3 direction = (transform.position - prevPosition).normalized;

        if (collision.rigidbody != null)
        {
            EnemyScript es = hitGO.GetComponent<EnemyScript>();
            if (es != null)
            {
                es.Damage((int)velocity.sqrMagnitude * (int)damageCurve.Evaluate((int)recoveryStage));
                
                es.hitAudio.PlayOneShot(hitAudio.clip);
                es.shouldFollow = false;
                StartCoroutine(ReEnableFollow(es));
            }
            collision.rigidbody.AddForceAtPosition(direction * velocity.sqrMagnitude,collision.contacts[0].point, ForceMode.Impulse);
        }

       
    }

    IEnumerator ReEnableFollow(EnemyScript es)
    {
        yield return new WaitForSeconds(2);
        es.shouldFollow = true;
    }
}
