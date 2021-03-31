using UnityEngine;
using UnityEngine.EventSystems;

public class movement_board : MonoBehaviour
{
    private GameObject camera;
    private GameObject player;
    private float MouseX;
    private float MouseY;
    private int sensitivity = 3;
    public float mousePositionX;
    public float mousePositionY;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mousePositionX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        mousePositionY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        MouseX = Input.GetAxisRaw("Mouse X");
        MouseY = Input.GetAxisRaw("Mouse Y");
        if (Input.GetKey(KeyCode.Mouse0)) //przesuwa kamera przy trzymaniu myszki
        {
            camera.GetComponent<Transform>().position = new Vector3(camera.GetComponent<Transform>().position.x - (MouseX / sensitivity * camera.GetComponent<Camera>().orthographicSize / 3), camera.GetComponent<Transform>().position.y - (MouseY / sensitivity * camera.GetComponent<Camera>().orthographicSize / 3), -10);
        }
        if(Input.GetKey(KeyCode.R))
        {
            camera.GetComponent<Transform>().position = new Vector3(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y, -10); //resetuje pozycje kamery do pozycji gracza
        }
        if(Input.GetKeyDown(KeyCode.T)) //wlacza / wylacza skrypt
        {
            if(player.GetComponent<movement>().enabled == false)
            {  
                player.GetComponent<movement>().enabled = true;
            }
            else
            {
                player.GetComponent<movement>().enabled = false;
            }
        }
        if(Input.mouseScrollDelta.y != 0)
        {
            if(camera.GetComponent<Camera>().orthographicSize < 3)
            {
                camera.GetComponent<Camera>().orthographicSize = 3;
            }
            if(camera.GetComponent<Camera>().orthographicSize >= 3)
            {
                camera.GetComponent<Camera>().orthographicSize -= Input.mouseScrollDelta.y * 2;
            }
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.right, 1f);
            Destroy(hit.transform.gameObject);
        }
    }
}
