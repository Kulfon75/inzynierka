using UnityEngine;

public class movement_board : MonoBehaviour
{
    private GameObject camera;
    private GameObject player;
    private float MouseX;
    private float MouseY;
    private int sensitivity = 3;
    public float mousePositionX;
    public float mousePositionY;
    public Canvas hud;
    private RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        hud.enabled = false;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // FixedUpdate is called multiple times per frame
    void FixedUpdate()
    {
        
    }
    // Update is  called once per frame
    private void Update()
    {
        MouseX = Input.GetAxisRaw("Mouse X");
        MouseY = Input.GetAxisRaw("Mouse Y");
        if (Input.GetKey(KeyCode.Mouse0)) //przesuwa kamera przy trzymaniu myszki
        {
            camera.GetComponent<Transform>().position = new Vector3(camera.GetComponent<Transform>().position.x - (MouseX / sensitivity * camera.GetComponent<Camera>().orthographicSize / 3), camera.GetComponent<Transform>().position.y - (MouseY / sensitivity * camera.GetComponent<Camera>().orthographicSize / 3), -10);
        }
        if (Input.GetKey(KeyCode.R))
        {
            camera.GetComponent<Transform>().position = new Vector3(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y, -10); //resetuje pozycje kamery do pozycji gracza
        }
        if (Input.GetKeyDown(KeyCode.T)) //wlacza / wylacza skrypt
        {
            if (player.GetComponent<movement>().enabled == false)
            {
                player.GetComponent<movement>().enabled = true;
            }
            else
            {
                player.GetComponent<movement>().enabled = false;
            }
        }
        if (Input.mouseScrollDelta.y != 0)
        {
            if (camera.GetComponent<Camera>().orthographicSize < 3)
            {
                camera.GetComponent<Camera>().orthographicSize = 3;
            }
            if (camera.GetComponent<Camera>().orthographicSize >= 3)
            {
                camera.GetComponent<Camera>().orthographicSize -= Input.mouseScrollDelta.y * 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(hit != false && (hit.transform.tag == "PlaceRoom" || hit.transform.tag == "Room"))
            {
                hit.transform.gameObject.GetComponent<mouse_block>().IsActive(false);
            }
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.right, 0.1f); //ustawia raycast na pozycje myszki
            if (hit != false && (hit.transform.tag == "PlaceRoom" || hit.transform.tag == "Room"))
            {
                if(hit.transform.tag == "PlaceRoom")
                {
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    hud.enabled = true;
                    hud.GetComponent<hud>().block = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<mouse_block>().IsActive(true);
                }
                else
                {
                    //wprowadzic dodawania plapek - wyswietlanie sie odpowiedniego okna do dodawania ich (przeciaganie myszka na odpowiednie miejsce w pokoju? fokusowanie kamery na dany pokuj i lokowanie jej w tym miejscu?)
                }
            }
            else
            {
                hud.enabled = false;
            }
        }
    }
}