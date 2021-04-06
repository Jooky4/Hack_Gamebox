using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshManager : MonoBehaviour
{
    //[SerializeField]
   // private Mesh mesh;
    //private MeshFilter meshFilter;
    [SerializeField]
    private Transform []bodyParts;




    // Start is called before the first frame update
    void Start()
    {
       // meshFilter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
       // meshFilter.mesh = mesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Modification")
        {
            string modificatorName = other.name;
            foreach (Transform bodyPart in bodyParts)
            {
                if (bodyPart.name == modificatorName)
                {
                    MeshFilter meshFilterBodyPart = bodyPart.GetComponent<MeshFilter>();
                    MeshFilter meshFilterModificator = other.GetComponent<MeshFilter>();

                    //Mesh meshBodyPart;
                    //Mesh meshModificatorName;

                    meshFilterBodyPart.mesh = meshFilterModificator.mesh;

                    other.gameObject.SetActive(false);

                }
            }

            

            
            //  Debug.Log(other.name);

        }
    }

}
