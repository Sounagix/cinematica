using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFarolas : MonoBehaviour
{
    [SerializeField]
    private Light[] farolas;

    [SerializeField]
    private AudioClip[] cortoCircuitoSonidos;

    [SerializeField]
    private float minCortoCircuitoTiempo;

    [SerializeField]
    private float maxCortoCircuitoTiempo;

    private void Start()
    {
        Invoke(nameof(GestionaFarolas), Random.Range(minCortoCircuitoTiempo, maxCortoCircuitoTiempo));
    }


    private void GestionaFarolas()
    {
        int numFarolas = Random.Range(0,farolas.Length);
        bool apagaFarola = false;
        for (int i = 0; i < farolas.Length; i++) 
        {
            apagaFarola = Random.Range(0, 2) == 0 ? false : true;
            if (apagaFarola)
            {
                farolas[i].intensity = 0;
                farolas[i].GetComponent<ParticleSystem>().Play();
                int elecionSonido = Random.Range(0,cortoCircuitoSonidos.Length);
                farolas[i].GetComponent<AudioSource>().PlayOneShot(cortoCircuitoSonidos[elecionSonido]);
            }
            else
            {
                farolas[i].intensity = 1;
            }
        }
        Invoke(nameof(GestionaFarolas), Random.Range(minCortoCircuitoTiempo, maxCortoCircuitoTiempo));
    }

}
