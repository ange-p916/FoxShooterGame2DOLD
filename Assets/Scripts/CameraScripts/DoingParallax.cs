using UnityEngine;
using System.Collections;

public class DoingParallax : MonoBehaviour {

    public GameObject[] images;
    float[] parallaxScales;
    public float speed = 0.002f;
    Camera cam;

    public float smoothing;

    private Vector3 prevCamPos;

    void Start()
    {

        parallaxScales = new float[images.Length];
        cam = Camera.main;

        for (int i = 0; i < images.Length; i++)
        {
            parallaxScales[i] = images[i].transform.position.z * -1f;
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < images.Length; i++)
        {
            var parallax = (prevCamPos.x - cam.transform.position.x) * parallaxScales[i];
            var bgtargetPosX = images[i].transform.position.x + parallax * -1;

            Vector3 bgTargetPos = new Vector3(bgtargetPosX, images[i].transform.position.y, images[i].transform.position.z);

            images[i].transform.position = Vector3.Lerp(images[i].transform.position, bgTargetPos, smoothing * Time.deltaTime);
        }
        prevCamPos = cam.transform.position;
    }
}
