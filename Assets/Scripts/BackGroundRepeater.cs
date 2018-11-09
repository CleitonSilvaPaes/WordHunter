using UnityEngine;

public class BackGroundRepeater : MonoBehaviour {
    public float offset = 19.2f;
    private Transform cameraTransform;

	// Use this for initialization
	void Start () {
        cameraTransform = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x + offset < cameraTransform.position.x)
        {
            Vector3 newPos = transform.position;
            newPos.x += 2 * offset;
            transform.position = newPos;
        }
	}
}
