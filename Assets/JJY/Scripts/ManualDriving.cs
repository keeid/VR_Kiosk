using UnityEngine;
using UnityEngine.InputSystem;

public class ManualDriving : MonoBehaviour
{
    public float maxTorque;
	public Transform centerOfMass;
	public WheelCollider[] wheelColliders = new WheelCollider[4];
	public Transform[] tireMeshes = new Transform[4];

    Vector2 inVec;
    float steer;
    float accelerate;

    void OnCarDrive(InputValue value)
    {
        inVec = value.Get<Vector2>();
        accelerate = inVec.y;
    }

    void OnCarRotate(InputValue value)
    {
		inVec = value.Get<Vector2>();
		steer = inVec.x;
	}

    void Start()
	{
		GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
	}

	void FixedUpdate()
	{
		float finalAngle = steer * 45f;
		wheelColliders[0].steerAngle = finalAngle;
		wheelColliders[1].steerAngle = finalAngle;

		for (int i = 0; i < 4; i++)
		{
			wheelColliders[i].motorTorque = accelerate * maxTorque;
		}
	}

	void Update()
	{
        for (int i = 0; i < 4; i++)
		{
			Quaternion quat;
			Vector3 pos;
			wheelColliders[i].GetWorldPose(out pos, out quat);

			tireMeshes[i].position = pos;
			tireMeshes[i].rotation = quat;			
		}
	}	
}
