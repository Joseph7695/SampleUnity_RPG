using UnityEngine.EventSystems; 
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public LayerMask movementMask;
    public Interactable focused;

    Camera cam;
    PlayerMotor motor;


	// Use this for initialization
	void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //Debug.Log("We hit" + hit.collider.name + " at " + hit.point);
                // Move our player to what we hit
                motor.MoveToPoint(hit.point);
               // Debug.Log("We hit" + hit.collider.name + " at " + hit.point);

                // Focus an item & Unfocus other item
                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // Check if hit Interactable objects
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                // Set as focus if it is
                if(interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    private void RemoveFocus()
    {
        if (focused != null)
        {
            focused.OnDefocused();
        }
        focused = null;
        motor.StopFollowTarget();
    }

    private void SetFocus(Interactable newFocus)
    {
        if(newFocus != focused)
        {
            if (focused != null)
            {
                focused.OnDefocused();
            }
            focused = newFocus;
            motor.FollowTarget(newFocus);
        }
        newFocus.OnFocused(transform);
    }
}