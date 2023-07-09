using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private Vector3 ogMainCamPos;
    private Quaternion ogMainCamRot;

    // Start is called before the first frame update
    void Start()
    {
        //Store original main camera position
        ogMainCamPos = Camera.main.transform.position;
        ogMainCamRot = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CameraLerpTo(Transform oldTransform, Transform newTransform, float time)
    {
        float timer = 0.0f;


        while (timer < time)
        {
            float t = timer / time;
            Camera.main.transform.position = Vector3.Lerp(oldTransform.position, newTransform.transform.position, t);
            Camera.main.transform.rotation = Quaternion.Slerp(oldTransform.rotation, newTransform.transform.rotation, t);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator CameraLerpToOriginal(Transform oldTransform, float time)
    {
        float timer = 0.0f;


        while (timer < time)
        {
            float t = timer / time;

            Camera.main.transform.position = Vector3.Lerp(oldTransform.position, ogMainCamPos, t);
            Camera.main.transform.rotation = Quaternion.Slerp(oldTransform.rotation, ogMainCamRot, t);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator CameraLerpFromOriginal(Transform newTransform, float time)
    {
        float timer = 0.0f;


        while(timer < time)
        {
            float t = timer / time;

            Camera.main.transform.position = Vector3.Lerp(ogMainCamPos, newTransform.transform.position, t);
            Camera.main.transform.rotation = Quaternion.Slerp(ogMainCamRot, newTransform.transform.rotation, t);
            timer += Time.deltaTime;
            yield return null;
        }
    }
    
    public IEnumerator CameraLerpToRodEvent(Transform newTransform, float time)
    {
        float timer = 0.0f;


        while(timer < time)
        {
            float t = timer / time;

            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newTransform.transform.position, t);
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, newTransform.transform.rotation, t);
            timer += Time.deltaTime;
            yield return null;
        }
    }
    
}
