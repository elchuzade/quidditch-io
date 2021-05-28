using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraResizer : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject ball;

    [SerializeField] bool shopScene;

    [SerializeField] Camera mainCamera;

    void Start()
    {
        //Camera.main.orthographicSize = Screen.height / 2;
        //transform.position = new Vector3((float)Screen.width / 2, (float)Screen.height / 2, -10);

        //Change the camera zoom based on the screen ratio, for very tall or very wide screens
        if ((float)Screen.height / Screen.width > 2)
        {
            mainCamera.orthographicSize = 800;
        }
        else
        {
            if (shopScene)
            {
                mainCamera.orthographicSize = Screen.height / 2;
            }
            else
            {
                mainCamera.orthographicSize = 667;
            }
        }

        // Tablet screens
        if ((float)Screen.width / Screen.height > 0.7 || (float)Screen.width / Screen.height < 0.5)
        {
            canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        }
        else
        {
            // Phone screens
            canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
        }

        // Place a camera in the center of the shop scene so the ball is rendered correctly
        if (shopScene)
        {
            transform.position = new Vector3(Screen.width / 2, Screen.height / 2 - 100, -600);
        }
    }
}
