using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using SO.Events;
using Food;

namespace Player
{
    public class PlayerArmsDetection : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------	
        public Transform leftHand;
        public Transform rightHand;
        public Transform rayPosition;
        public Transform ThisCamera;
        public float rayDistance;
        public LayerMask objectToDetect;
        [ReadOnly] public bool objectDetected;        
        [Tooltip("The increment of the raycast lenght when player looks down or up")]
        public float rayRecalculationValue;
        public float angleOfRecalculation = 45;
        [ReadOnly] public float maxRayDistance;
        [Header("Events")]
        public SO_event onDrop;
        [Header("Debug")]
        //public Text text;
        #endregion

        #region NO_Inspector variables ------------------------------------------------
        RaycastHit hit;
        RaycastHit ohterHit;
        GameObject objectInLeftHand;
        GameObject objectInRightHand;
        PlayerInputsManager inputs;
        bool rightHandHasAnObject;
        bool leftHandHasAnObject;
        bool rotateButtonPressed;
        bool leftHandButtonPressed;
        bool rightHandButtonPressed;
        float originalRayDistance;
        #endregion

        #region methods ---------------------------------------------------------------
        private void Awake()
        {
            inputs = GetComponent<PlayerInputsManager>();
            originalRayDistance = rayDistance;
        }

        private void OnEnable()
        {
            inputs.rightArm.performed += RightHand;
            inputs.rightArm.performed += ctx => rightHandButtonPressed = true;
            inputs.rightArm.canceled += ctx => rightHandButtonPressed = false;

            inputs.lefttArm.performed += LeftHand;
            inputs.lefttArm.performed += ctx => leftHandButtonPressed = true;
            inputs.lefttArm.canceled += ctx => leftHandButtonPressed = false;

            inputs.rotate.performed += ctx => rotateButtonPressed = true;
            inputs.rotate.canceled += ctx => rotateButtonPressed = false;
        }

        private void OnDisable()
        {
            inputs.rightArm.performed -= RightHand;
            inputs.rightArm.performed -= ctx => rightHandButtonPressed = true;
            inputs.rightArm.canceled -= ctx => rightHandButtonPressed = false;

            inputs.lefttArm.performed -= LeftHand;
            inputs.lefttArm.performed -= ctx => leftHandButtonPressed = true;
            inputs.lefttArm.canceled -= ctx => leftHandButtonPressed = false;

            inputs.rotate.performed -= ctx => rotateButtonPressed = true;
            inputs.rotate.canceled -= ctx => rotateButtonPressed = false;
        }

        private void Update()
        {
            DetectAnObject();
            RecalculateRayDistance();
            RotateObject();
        }

        void RecalculateRayDistance()
        {
            if (Mathf.Abs(ThisCamera.localRotation.x) > angleOfRecalculation)
            {
                maxRayDistance = Mathf.Abs(ThisCamera.localRotation.x) * rayRecalculationValue;
                rayDistance = originalRayDistance + maxRayDistance;
            }
            else
            {
                rayDistance = originalRayDistance;
            }
        }

        void DetectAnObject()
        {
            Debug.DrawRay(rayPosition.position, rayPosition.forward * rayDistance, Color.red);
            if (Physics.Raycast(rayPosition.position, rayPosition.forward, rayDistance, objectToDetect))// layer 7 is dinamic
            {
                objectDetected = true;
            }
            else
            {
                objectDetected = false;
            }
        }

        void RightHand(InputAction.CallbackContext ctx)
        {
            if (rightHandHasAnObject == false)
            {
                if (objectDetected)
                {
                    objectInRightHand = GrabAnObject();
                    if (objectInRightHand != null)
                    {
                        AttachObjectToHand(rightHand, objectInRightHand);
                        rightHandHasAnObject = true;
                    }
                }
            }
            else
            {
                if (rotateButtonPressed == false)
                {
                    UnattachObject(objectInRightHand);
                    rightHandHasAnObject = false;
                }
            }
        }
        void LeftHand(InputAction.CallbackContext ctx)
        {
            if (leftHandHasAnObject == false)
            {
                if (objectDetected)
                {
                    objectInLeftHand = GrabAnObject();
                    if (objectInLeftHand != null)
                    {
                        AttachObjectToHand(leftHand, objectInLeftHand);
                        leftHandHasAnObject = true;
                    }
                }
            }
            else
            {
                if (rotateButtonPressed == false)
                {
                    UnattachObject(objectInLeftHand);
                    leftHandHasAnObject = false;
                }
            }
        }

        GameObject GrabAnObject()
        {
            if (Physics.Raycast(rayPosition.position, rayPosition.forward, out hit, rayDistance))
            {
                if (hit.collider.gameObject.layer == 7) // to verify the object is dinamic      
                {
                    var objectToGrab = hit.collider.gameObject;
                    //text.text = objectToGrab.name;
                    objectToGrab.transform.parent = null;
                    return objectToGrab.gameObject;
                }
            }
            return null;
        }

        void AttachObjectToHand(Transform hand, GameObject objectOnHand)
        {
            var otherObjectComponents = objectOnHand.GetComponentInChildren<FoodAtributes>();
  //          if (objectOnHand.gameObject.tag != "Plate")
            {
                objectOnHand.GetComponent<FoodParenting>().RiseOnParentedEvent();
                objectOnHand.GetComponent<FoodParenting>().QuitObjectFromIngredientList();
            }
            objectOnHand.transform.position = hand.position;
            objectOnHand.transform.parent = hand;
            objectOnHand.transform.localScale = Vector3.one * otherObjectComponents.ObjectSizeOnHand;
            objectOnHand.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        void UnattachObject(GameObject objectOnHand)
        {
            var otherObjectComponents = objectOnHand.GetComponentInChildren<FoodAtributes>();
//            if (objectOnHand.gameObject.tag != "Plate")
            {
                objectOnHand.GetComponent<FoodParenting>().RiseOnUnparentedEvent();
            }
            objectOnHand.transform.parent = null;
            if (Physics.Raycast(rayPosition.position, rayPosition.forward, out ohterHit, rayDistance))
            {
                // to leave the object in hand in a surface if player is enough close
                // else it just drop it
                objectOnHand.transform.position = ohterHit.point;
            }
            onDrop.Rise();
            objectOnHand.transform.localScale = Vector3.one;
        }

        void RotateObject()
        {
            if (rotateButtonPressed)
            {
                if (leftHandButtonPressed && objectInLeftHand != null)
                {
                    Vector2 mousePosition = inputs.look.ReadValue<Vector2>();
                    objectInLeftHand.transform.Rotate(new Vector3(mousePosition.x, inputs.scroll.ReadValue<Vector2>().y, mousePosition.y));
                }
                else if (rightHandButtonPressed && objectInRightHand != null)
                {
                    Vector2 mousePosition = inputs.look.ReadValue<Vector2>();
                    objectInRightHand.transform.Rotate(new Vector3(mousePosition.x, inputs.scroll.ReadValue<Vector2>().y, mousePosition.y));
                }
            }
        }
        #endregion
    }
}