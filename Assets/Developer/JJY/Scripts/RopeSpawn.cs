using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RopeSpawn : MonoBehaviour
{
    public GameObject part;
    public Transform gasGun;

    public int length = 1;
    public float dist = 0.21f;

    void Start()
    {
        Spawn();       
    }

    public void Spawn()
    {
        int count = (int)(length / dist);        

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + dist * i, transform.position.z);
            GameObject obj = Instantiate(part, pos, Quaternion.identity, transform);
            obj.transform.eulerAngles = new Vector3(180,0,0);
            obj.name = "Part" + i; 

            if (i == 0) // First part
            {
                Destroy (obj.GetComponent<CharacterJoint>());
                obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;             
            }
            else
            {
                obj.GetComponent<CharacterJoint>().connectedBody = transform.Find("Part"+(i-1)).GetComponent<Rigidbody>();               
            }
        }

        // Last part        
        Transform lastChild = transform.Find("Part" + (count - 1));
        lastChild.position = gasGun.position;
        lastChild.localScale = new Vector3(0.15f, 0.1f, 0.15f);
        lastChild.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        lastChild.gameObject.AddComponent<XRGrabInteractable>();
        lastChild.gameObject.GetComponent<XRGrabInteractable>().interactionLayers = 2; // GasDun

        gasGun.position = lastChild.position;
        gasGun.parent = lastChild;
    }
}
