using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.Input;

public class BoxCasterScript : MonoBehaviour
{
    [SerializeField] private HandRef hand;
    [SerializeField] private Transform directionTransform;
    private Material targetObjMat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;

        /*if (!Physics.BoxCast(hand.transform.position, new Vector2(5, 100), directionTransform.eulerAngles, out hitInfo, 100))
        {
            targetObjMat
        }*/
        
    }
}
