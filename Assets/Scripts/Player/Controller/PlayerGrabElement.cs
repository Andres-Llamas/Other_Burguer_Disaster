using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using Singletons;
using Managers;

namespace Player
{
    public class PlayerGrabElement : MonoBehaviour
    {
        #region Inspector variables ---------------------------------------------------
        [Header("Set variables")]
        [SerializeField] Transform _objectFromToShootRay; // this the object that is on the center of the screen that works as an aim
        [SerializeField] Transform leftArmTip, rightArmTip;
        [SerializeField][Tag] string ingredientTag;
        [SerializeField][Tag] string ingredientBaseTag;
        [Header("Grabbing settings")]
        [SerializeField] float _rayDetectionDistance = 1.5f;
        [SerializeField][Layer] int _rayDetectionLayer;// grabbable
        //[SerializeField] float droppedObjectObsetYDistance = 0.1f;
        [SerializeField] float _moveObjectTowardsArmInterpolation = 0.2f;
        [SerializeField] float _rotateObjectSpeedOnMouse = 1;
        [SerializeField] float _rotateObjectSpeedOnController = 2;

        [Header("Debugging")]
        [SerializeField][ReadOnly] GameObject _objectBeingHoldedByLeftArm;
        [SerializeField][ReadOnly] GameObject _objectBeingHoldedByRightArm;
        [SerializeField][ReadOnly] RaycastHit _detectedElementHit;// this is just the object(hit) that is currently being returned by the raycast
        [SerializeField][ReadOnly] GameObject _detectedGrabbableObject;// this object is being assigned once the _detectedElementHit object has the same layer as _rayDetectionLayer
        #endregion

        #region NO_Inspector variables ------------------------------------------------        
        Image _uiCrosshairAim;
        InputManager _inputs;
        #endregion

        #region methods ---------------------------------------------------------------        
        private void Awake()
        {
            _inputs = MasterSingleton.Instance.InputManager;
        }
        private void Start()
        {
            _uiCrosshairAim = _objectFromToShootRay.GetComponent<Image>();
            _inputs = MasterSingleton.Instance.InputManager;
        }

        private void OnEnable()
        {
            _inputs.leftArmActions += GrabOrDropObjectLeftArm;
            _inputs.rightArmActions += GrabOrDropObjectRightArm;
        }

        private void OnDisable()
        {
            _inputs.leftArmActions -= GrabOrDropObjectLeftArm;
            _inputs.rightArmActions -= GrabOrDropObjectRightArm;
        }

        private void Update()
        {
            RotateGrabbedObject();
        }

        private void FixedUpdate()
        {
            DetectElement();
        }

        void DetectElement()
        {
            //This method is intended to be used in Fixed Update
            // this shoots a raycast and get the hit, after that it checks if the hit gameObject can be grabbed by checking its layer.
            // if the hit gameObject can indeed be grabbed, then it stores that gameObject in the "_detectedGrabbableObject" variable in order to be used by the other functions
            // also if the object can be grabbed, this changes the color of the crosshair at the center of the screen to green
            bool elementDetected = Physics.Raycast(_objectFromToShootRay.position, _objectFromToShootRay.transform.forward * _rayDetectionDistance, out _detectedElementHit, _rayDetectionDistance);
            GameObject detectedObject = null;
            if (elementDetected)// if the ray has detected something
            {
                detectedObject = _detectedElementHit.transform.gameObject;
                if (detectedObject.layer == _rayDetectionLayer)// if the object is grabbable
                {
                    _detectedGrabbableObject = detectedObject;
                    _uiCrosshairAim.color = Color.green;
                }
                else
                {
                    _detectedGrabbableObject = null;
                    _uiCrosshairAim.color = Color.white;
                }
            }
            else
            {
                _detectedGrabbableObject = null;
                _uiCrosshairAim.color = Color.white;
            }
        }

        void GrabOrDropObjectLeftArm()
        {
            // if the arm is not holding an object, it can hold a new one.
            // if the arm is holding an object, then drop the object is already holding
            if (_inputs.IsRotateLeftObjectControllerPressed == false && _inputs.IsRotateObjectKeyboardPressed == false)
            {
                if (_objectBeingHoldedByRightArm == null)
                {
                    if (_objectBeingHoldedByLeftArm == null)
                        AttachGrabbableObjectToArm("left");
                    else
                        UnlatchGrabbableObjectFromArm(_objectBeingHoldedByLeftArm, "left");
                }
                else
                {
                    if (_objectBeingHoldedByRightArm != _detectedGrabbableObject)
                    {
                        if (_objectBeingHoldedByLeftArm == null)
                            AttachGrabbableObjectToArm("left");
                        else
                            UnlatchGrabbableObjectFromArm(_objectBeingHoldedByLeftArm, "left");
                    }
                }
            }
        }

        void GrabOrDropObjectRightArm()
        {
            // if the arm is not holding an object, it can hold a new one.
            // if the arm is holding an object, then drop the object is already holding
            if (_inputs.IsRotateRightObjectControllerPressed == false && _inputs.IsRotateObjectKeyboardPressed == false)
            {
                if (_objectBeingHoldedByLeftArm == null)
                {
                    if (_objectBeingHoldedByRightArm == null)
                        AttachGrabbableObjectToArm("right");
                    else
                        UnlatchGrabbableObjectFromArm(_objectBeingHoldedByRightArm, "right");
                }
                else
                {
                    if (_objectBeingHoldedByLeftArm != _detectedGrabbableObject)
                    {
                        if (_objectBeingHoldedByRightArm == null)
                            AttachGrabbableObjectToArm("right");
                        else
                            UnlatchGrabbableObjectFromArm(_objectBeingHoldedByRightArm, "right");
                    }
                }
            }
        }

        void AttachGrabbableObjectToArm(string armSide)
        {
            if (_detectedGrabbableObject != null)
            {
                _detectedGrabbableObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //_detectedGrabableObject.transform.position = armToAttachIt.position;                
                if (armSide.Equals("left"))
                {
                    _objectBeingHoldedByLeftArm = _detectedGrabbableObject;
                    InvokeRepeating(nameof(MoveGrabbableObjectTowardsLeftArm), 0, Time.deltaTime);
                    _detectedGrabbableObject.transform.parent = leftArmTip;
                }
                else
                {
                    _objectBeingHoldedByRightArm = _detectedGrabbableObject;
                    InvokeRepeating(nameof(MoveGrabbableObjectTowardsRightArm), 0, Time.deltaTime);
                    _detectedGrabbableObject.transform.parent = rightArmTip;
                }
                _detectedGrabbableObject.gameObject.GetComponent<Collider>().isTrigger = true;
            }
        }

        void UnlatchGrabbableObjectFromArm(GameObject objectToBeUnlatched, string armSide)
        {
            RaycastHit surfaceToBeDropped;
            bool surfaceToBeDroppedDetected = Physics.Raycast(_objectFromToShootRay.position, _objectFromToShootRay.transform.forward * _rayDetectionDistance, out surfaceToBeDropped, _rayDetectionDistance);
            objectToBeUnlatched.transform.parent = null;
            objectToBeUnlatched.transform.localScale = Vector3.one;
            if (surfaceToBeDroppedDetected)
            {
                objectToBeUnlatched.transform.position = surfaceToBeDropped.point;
                if (surfaceToBeDropped.transform.gameObject.tag.Equals(ingredientTag) || surfaceToBeDropped.transform.gameObject.tag.Equals(ingredientBaseTag))
                {
                    objectToBeUnlatched.gameObject.GetComponent<Collider>().isTrigger = true;
                    objectToBeUnlatched.transform.position = FixPositionOfDroppedObject(objectToBeUnlatched.transform, surfaceToBeDropped.transform);
                    objectToBeUnlatched.GetComponent<Rigidbody>().isKinematic = true;
                    objectToBeUnlatched.transform.parent = surfaceToBeDropped.transform;
                }
                else
                {
                    objectToBeUnlatched.gameObject.GetComponent<Collider>().isTrigger = false;
                    objectToBeUnlatched.GetComponent<Rigidbody>().isKinematic = false;
                }
            }
            else
            {
                objectToBeUnlatched.gameObject.GetComponent<Collider>().isTrigger = false;
                objectToBeUnlatched.GetComponent<Rigidbody>().isKinematic = false;
            }

            if (armSide.Equals("left"))
            {
                _objectBeingHoldedByLeftArm = null;
                CancelInvoke(nameof(MoveGrabbableObjectTowardsLeftArm));
            }
            else
            {
                _objectBeingHoldedByRightArm = null;
                CancelInvoke(nameof(MoveGrabbableObjectTowardsRightArm));
            }
        }

        // I could use coroutines, but that is more laborious, so I created a lot of methods. I know it can be reduced but mhe...
        void MoveGrabbableObjectTowardsLeftArm()
        {
            // this is to attach the grabbable obejt towards the arm in a smooth animation
            if (Mathf.Abs(_objectBeingHoldedByLeftArm.transform.position.magnitude - leftArmTip.position.magnitude) < 0.01f)
            {
                _objectBeingHoldedByLeftArm.transform.position = leftArmTip.position;
                CancelInvoke(nameof(MoveGrabbableObjectTowardsLeftArm));
            }
            else
            {
                _objectBeingHoldedByLeftArm.transform.transform.position = Vector3.Lerp(_objectBeingHoldedByLeftArm.transform.position, leftArmTip.position, _moveObjectTowardsArmInterpolation);
            }
        }

        void MoveGrabbableObjectTowardsRightArm()
        {
            // is the same function as "MoveGrabbableObjectToFromLeftArm", but with different name in order to avoid problems with Invoke;
            if (Mathf.Abs(_objectBeingHoldedByRightArm.transform.position.magnitude - rightArmTip.position.magnitude) < 0.01f)
            {
                _objectBeingHoldedByRightArm.transform.position = rightArmTip.position;
                CancelInvoke(nameof(MoveGrabbableObjectTowardsRightArm));
            }
            else
            {
                _objectBeingHoldedByRightArm.transform.transform.position = Vector3.Lerp(_objectBeingHoldedByRightArm.transform.position, rightArmTip.position, _moveObjectTowardsArmInterpolation);
            }
        }

        Vector3 FixPositionOfDroppedObject(Transform objectToBeFixed, Transform placeWereWasDropped)
        {
            Collider placeWereWasDroppedCollider = placeWereWasDropped.gameObject.GetComponent<Collider>();
            Vector3 pos = new Vector3(objectToBeFixed.position.x,
                                        objectToBeFixed.position.y + placeWereWasDroppedCollider.bounds.size.y * 0.25f,//TODO make it more consistent
                                        objectToBeFixed.position.z);
            print($"Fixing \"{objectToBeFixed.name} position to {objectToBeFixed.position.y + placeWereWasDroppedCollider.bounds.size.y * 0.25f}\"");
            return pos;
        }

        void RotateGrabbedObject()
        {
            if (_inputs.IsRotateObjectKeyboardPressed)
            {
                if (_inputs.IsLeftArmPressed && _objectBeingHoldedByLeftArm != null)
                {
                    _objectBeingHoldedByLeftArm.transform.Rotate(new Vector3(_inputs.LookInput.x, _inputs.Scroll.y, _inputs.LookInput.y) * _rotateObjectSpeedOnMouse);
                }
                else if (_inputs.IsRightArmPressed && _objectBeingHoldedByRightArm != null)
                {
                    _objectBeingHoldedByRightArm.transform.Rotate(new Vector3(_inputs.LookInput.x, _inputs.Scroll.y, _inputs.LookInput.y) * _rotateObjectSpeedOnMouse);
                }
            }
            else if (_inputs.IsRotateLeftObjectControllerPressed && _objectBeingHoldedByLeftArm != null)
            {
                _objectBeingHoldedByLeftArm.transform.Rotate(new Vector3(_inputs.LookInput.x, _inputs.Scroll.y, _inputs.LookInput.y) * _rotateObjectSpeedOnController);
            }
            else if (_inputs.IsRotateRightObjectControllerPressed && _objectBeingHoldedByRightArm != null)
            {
                _objectBeingHoldedByRightArm.transform.Rotate(new Vector3(_inputs.LookInput.x, _inputs.Scroll.y, _inputs.LookInput.y) * _rotateObjectSpeedOnController);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (_objectFromToShootRay != null)
                Gizmos.DrawRay(_objectFromToShootRay.position, _objectFromToShootRay.forward * _rayDetectionDistance);
        }
        #endregion
    }
}