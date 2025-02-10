using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
   
    Vector2 direction = Vector2.right;

    List<Transform> segments;

    [SerializeField] Transform SegmentTranform;
    [SerializeField] int initialSize = 5;
    [SerializeField] bool forMobile = false;
    private Vector2 startTouchPosition, endTouchPosition;

    private void Start()
    {
        segments = new List<Transform>();
        InitialState();
    }
    private void Update()
    {
        if(forMobile)
        {
            touchControls();
        }
        else
        {
            controllInput();
        }
       
    }

    private void FixedUpdate()
    {
        for(int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
            segments[i].rotation = segments[i - 1].rotation;
        }
        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(this.transform.position.y) + direction.y, 0);
    }
    
    void controllInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
            transform.rotation = Quaternion.Euler(0, 0, 90f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
            transform.rotation = Quaternion.Euler(0, 0, -90f);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
            transform.rotation = Quaternion.Euler(0, 0, 180f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
            transform.rotation = Quaternion.Euler(0, 0, 0f);
        }
    }

    void touchControls()
    {
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position; 
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                Vector2 swipeDelta = endTouchPosition - startTouchPosition;
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) // Horizontal Swipe
                {
                    if (swipeDelta.x > 0)
                    {
                        direction = Vector2.right;
                        transform.rotation = Quaternion.Euler(0, 0, 0f);
                    }

                    else
                    {
                        direction = Vector2.left;
                        transform.rotation = Quaternion.Euler(0, 0, 180f);
                    }
                }

                else // Vertical Swipe
                {
                    if (swipeDelta.y > 0)
                    {
                        direction = Vector2.up;
                        transform.rotation = Quaternion.Euler(0, 0, 90f);
                    }
                    else
                    {
                        direction = Vector2.down;
                        transform.rotation = Quaternion.Euler(0, 0, -90f);
                    }
                }
            }
            
        }
    }

    void Grow()
    {
        Transform segment = Instantiate(this.SegmentTranform);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);  

        
    }

    void InitialState()
    {
        for (int i = 0; i< segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);
        for(int i = 0; i < initialSize; i++)
        {
            segments.Add(Instantiate(SegmentTranform));
        }
        this.transform.position = Vector3.zero;
    }
    //grow funtion called after eating food
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Grow();
        }

        else if (collision.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
