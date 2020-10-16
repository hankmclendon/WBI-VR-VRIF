using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float speed = 40;
    public GameObject bullet;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public GameObject holster;
    public float lerpTime = 0.5f;

    private float currentLerpTime;

    //public LayerMask layerMask;
    private LineRenderer lineRenderer;
    private bool isPointingAtHolster = false;
    private bool isAttaching = false;
    private bool attach = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    public void Fire()
    {
        GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
        audioSource.PlayOneShot(audioClip);
        Destroy(spawnedBullet, 2);
    }

    public void SetScale()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    //public void CheckForHolster()
    //{
    //    Debug.Log("Check For Holster Called");

    //    if (isPointingAtHolster)
    //    {
    //        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    //        gameObject.GetComponent<Rigidbody>().useGravity = false;
            
    //        currentLerpTime = 0f;
    //        isAttaching = true;
    //        //transform.position = holster.transform.position;
    //        //transform.rotation = holster.transform.rotation;
    //        //transform.eulerAngles = new Vector3(holster.transform.eulerAngles.x, holster.transform.eulerAngles.y + 180, holster.transform.eulerAngles.z + 90);

    //    }
    //}

    //void FixedUpdate()
    //{
    //    // Bit shift the index of the layer (8) to get a bit mask
    //    //int layerMask = 1 << 8;

    //    // This would cast rays only against colliders in layer 8.
    //    // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
    //    //layerMask = ~layerMask;
    //    Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.left));

    //    RaycastHit hit;
    //    // Does the ray intersect any objects excluding the player layer
    //    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
    //    {
    //        if(hit.collider == holster.GetComponent<Collider>())
    //        {
    //            Debug.DrawLine(ray.origin, hit.point);
    //            //lineRenderer.enabled = true;
    //            lineRenderer.SetPosition(0, transform.position);
    //            lineRenderer.SetPosition(1, hit.point);
    //            lineRenderer.startWidth = 0.05f;
    //            lineRenderer.endWidth = 0.05f;
    //            isPointingAtHolster = true;
    //            //Debug.Log("Did Hit");
    //        }
    //    }
    //    else
    //    {
    //        Debug.DrawLine(ray.origin, hit.point);
    //        lineRenderer.startWidth = 0.00f;
    //        lineRenderer.endWidth = 0.00f;
    //        isPointingAtHolster = false;
    //        //Debug.Log("Did not Hit");
    //    }

    //    if (isAttaching)
    //    {
    //        currentLerpTime += Time.deltaTime;
    //        if (currentLerpTime > lerpTime)
    //        {
    //            currentLerpTime = lerpTime;
    //            isAttaching = false;
    //            gameObject.transform.SetParent(holster.transform);
    //        }

    //        //lerp!
    //        float perc = currentLerpTime / lerpTime;
    //        transform.position = Vector3.Lerp(transform.position, holster.transform.position, perc);
    //        Vector3 attachRotation = new Vector3(holster.transform.eulerAngles.x, holster.transform.eulerAngles.y + 180, holster.transform.eulerAngles.z + 90);
    //        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, attachRotation, perc);
    //    }
    //}

   
}
