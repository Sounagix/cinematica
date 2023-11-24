using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum STATES: int
{
    PLANO_GENERAL,
    PLANO_FAROLA,
    WALKING, 
    ZOOM,
    POV,
    ROT_POV,
}

public class CameraMaster : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cameras;

    [SerializeField]
    private CinemachineDollyCart brainCam;

    [SerializeField]
    private float tiempoPlanoGeneral, tiempoPlanoFarola, tiempoPlanoCaja;

    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject box;

    //[SerializeField]
    private int frames = 0;

    //[SerializeField]
    private int framesCount = 45;

    //[SerializeField]
    private float angleToRotate = 15.0f;

    private int index = 0;

    private STATES sTATES = STATES.PLANO_GENERAL;


    private void Start()
    {
        SwitchEvent();
    }

    public void OnTrigger()
    {
        index++;
        sTATES++;
        SwitchEvent();
    }

    //private void Update()
    //{
    //    if (sTATES.Equals(STATES.POV))
    //    {
    //        float ratio = frames / (float)framesCount;
    //        Vector3 rot = Vector3.Lerp(transform.rotation.eulerAngles, transform.rotation.eulerAngles + (Vector3.right * angleToRotate), ratio);
    //        frames = (frames + 1) % (framesCount + 1);
    //        brainCam.transform.Rotate(rot);
    //    }
    //}


    private void FixedUpdate()
    {
        if (sTATES.Equals(STATES.ZOOM) && brainCam.m_Position > 19.0f)
        {
            OnTrigger();
        }
    }

    private void SwitchEvent()
    {
        switch (sTATES)
        {
            case STATES.PLANO_GENERAL:
                cameras[index].gameObject.SetActive(true);
                Invoke(nameof(SwitchEvent), tiempoPlanoGeneral);
                index++;
                sTATES++;
                break;
            case STATES.PLANO_FAROLA:
                cameras[index - 1].gameObject.SetActive(false);
                cameras[index].gameObject.SetActive(true);
                Invoke(nameof(SwitchEvent), tiempoPlanoFarola);
                index++;
                sTATES++;
                break;
            case STATES.WALKING:
                cameras[index - 1].gameObject.SetActive(false);
                cameras[index].gameObject.SetActive(true);
                brainCam.m_Position = 0.0f;
                player.StartWalking();
                break;
            case STATES.ZOOM:
                cameras[index - 1].gameObject.SetActive(false);
                cameras[index].gameObject.SetActive(true);
                brainCam.m_Path = cameras[index].GetComponent<CinemachinePathBase>();
                brainCam.m_Position = 0.0f;
                break;
            case STATES.POV:
                cameras[index - 1].gameObject.SetActive(false);
                cameras[index].gameObject.SetActive(true);
                brainCam.m_Position = 0.0f;
                box.gameObject.SetActive(true);
                Invoke(nameof(SwitchEvent), tiempoPlanoCaja);
                index++;
                sTATES++;
                break;
            case STATES.ROT_POV:
                cameras[index - 1].gameObject.SetActive(false);
                cameras[index].gameObject.SetActive(true);
                break;
        }
    }
}
