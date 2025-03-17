using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
    public GameObject _mainCamera;

    private void LateUpdate()
    {
        Vector3 cameraPosition = _mainCamera.transform.position;

        cameraPosition.y = transform.position.y;

        transform.LookAt(cameraPosition);

        transform.Rotate(0f, 180f, 0f);
    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
