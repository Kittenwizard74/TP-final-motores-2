using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim_rotation : MonoBehaviour
{
    void Update()
    {
        HandleRotation();
    }

    protected virtual void HandleRotation()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.transform.position.y;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector3 direction = mouseWorldPosition - transform.position;
        direction.y = 0;

        Quaternion rotation = Quaternion.LookRotation(direction);

        transform.rotation = rotation;
    }
}
