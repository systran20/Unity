using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    public bool flashLightOn = true;
    public Light light;
    public Transform tf;    
    
    void Start()
    {

    }
    
    void Update()
    {
        //Checks if the boolean is true or false.
        if (flashLightOn == true)
        {
            light.enabled=true;
        }
        else
        {
            light.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashLightOn = !flashLightOn;
        }

    }
    void LateUpdate()
    {
        FollowMousePosition();
    }
    void FollowMousePosition()
    {
        tf.rotation = Quaternion.Euler(new Vector3(Input.mousePosition.x , Input.mousePosition.y , 0f));
        //this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //this.transform.position = Camera.main.ViewportToScreenPoint(Input.mousePosition);
        //this.transform.position = Camera.main.WorldToScreenPoint(Input.mousePosition);
    }
}
