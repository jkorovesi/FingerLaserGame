using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchLaser : MonoBehaviour {

    public Text tCount;

    GameObject gObj = null;

    public GameObject laser1;
    public GameObject laser2;
    public GameObject enemy;
    Plane objPlane;
    Vector3 mO;
    Vector3 m1;
    public LineRenderer linerenderer;

    Ray GenerateMouseRay(Vector3 touchPos)
    {
        Vector3 mousePosFar = new Vector3(touchPos.x, touchPos.y, Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(touchPos.x, touchPos.y, Camera.main.nearClipPlane);
        Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);

        Ray mr = new Ray(mousePosN, mousePosF - mousePosN);
        return mr;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //UnityEngine.Touch[] myTouches = Input.touches;

        tCount.text = Input.touchCount.ToString();

        if (Input.touchCount > 0 && Input.touchCount < 2)
        {


            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray mouseRay = GenerateMouseRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit) )
                {
                    objPlane = new Plane(Camera.main.transform.forward * -1, laser1.transform.position);


                    //calc touch offset
                    Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    float rayDistance;
                    objPlane.Raycast(mRay, out rayDistance);
                    laser1.transform.position = Vector3.Lerp(transform.position, hit.point, Time.time);
                    mO = laser1.transform.position - mRay.GetPoint(rayDistance);
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                float rayDistance;
                if (objPlane.Raycast(mRay, out rayDistance))
                    laser1.transform.position = mRay.GetPoint(rayDistance) + mO;

            }

            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                linerenderer.enabled = false;
            }
            
        }

        if (Input.touchCount > 1)
            {
                if (Input.GetTouch(1).phase == TouchPhase.Began)
                {

                    linerenderer.enabled = true;

                    //Draw touch ray?
                    Ray mouseRay = GenerateMouseRay(Input.GetTouch(1).position);
                    RaycastHit hit;

                    if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit))
                    {
                        objPlane = new Plane(Camera.main.transform.forward * -1, laser2.transform.position);


                        //calc touch offset
                        Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(1).position);
                        float rayDistance;
                        objPlane.Raycast(mRay, out rayDistance);
                        laser2.transform.position = Vector3.Lerp(transform.position, hit.point, Time.time);
                        m1 = laser2.transform.position - mRay.GetPoint(rayDistance);
                    }
                }
                else if (Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(1).position);
                    float rayDistance;
                    if (objPlane.Raycast(mRay, out rayDistance))
                    {
                        laser2.transform.position = mRay.GetPoint(rayDistance) + m1;
                    }
                       

                }
                else if (Input.GetTouch(1).phase == TouchPhase.Ended)
                {
                    linerenderer.enabled = false;
                }
            }

        if(linerenderer.enabled == true)
        {
            Vector3 raycastDir = laser2.transform.position - laser1.transform.position;
            Ray laserRay = new Ray(laser1.transform.position, raycastDir);
            RaycastHit hit1;
            Debug.DrawRay(laser1.transform.position, raycastDir);
            if (Physics.Raycast(laserRay.origin, laserRay.direction, out hit1))
            {
                print(hit1.collider.gameObject.name);
                enemy.GetComponent<EnemyHealth>().enemyHealth -= 1;
            }
        }
    }
}
