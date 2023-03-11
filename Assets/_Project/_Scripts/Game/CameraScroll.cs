using UnityEngine;

public class CameraScroll : MonoBehaviour {
    
    public float scrollSpeed = 5f;
    
    void Update () 
    {
        transform.Translate(Vector3.right * (scrollSpeed * Time.deltaTime));
    }
}
