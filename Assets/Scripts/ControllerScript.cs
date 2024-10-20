using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ControllerScript : MonoBehaviour
{
    public Camera sceneCamera;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float step;
    void Start()
        {
            transform.position = sceneCamera.transform.position + sceneCamera.transform.forward * 3.0f;
        }
    void centerCube()
    {
        targetPosition = sceneCamera.transform.position + sceneCamera.transform.forward * 3.0f;
        targetRotation = Quaternion.LookRotation(transform.position - sceneCamera.transform.position);

        transform.position = Vector3.Lerp(transform.position, targetPosition, step);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(sceneCamera.transform.position, sceneCamera.transform.forward, out hit, 100.0f))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // ヒットしたオブジェクトがターゲットの場合、処理を行う
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.OnHit(); // ヒット処理を呼び出し
            }
        }
    }

    void Update()
    {
        step = 5.0f * Time.deltaTime;
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            centerCube();
            Shoot();  // インデックストリガーを押したら弾を発射
        }
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) centerCube();
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft)) transform.Rotate(0, 5.0f * step, 0);
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight)) transform.Rotate(0, -5.0f * step, 0);
        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        }
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.0f)
        {
            transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        }
    }
}
