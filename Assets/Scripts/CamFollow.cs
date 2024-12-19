using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField]
    private Transform Target;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private Vector2 horizontalBounds;

    [SerializeField]
    private Vector2 verticalBounds;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target == null) return;

        if (Target.position.x >= horizontalBounds.x && Target.position.x <= horizontalBounds.y)
            this.transform.position = new Vector3(Target.position.x + offset.x, transform.position.y, transform.position.z);

        if (Target.position.y >= verticalBounds.x && Target.position.y <= verticalBounds.y)
            this.transform.position = new Vector3(this.transform.position.x, Target.position.y + offset.y, transform.position.z);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            AudioSettings.instance.ToggleSettings();
    }
}
