using UnityEngine;
//---------------
using UnityEngine.AI; //Necesario
public class PerseguirPlayer : MonoBehaviour
{
    NavMeshAgent navAgent;  //Controlador de capacidades de navegación de un objeto
    Transform transfPlayer;
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        transfPlayer = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        navAgent.destination = transfPlayer.position;
        //navAgent.SetDestination(transfPlayer.position); //Hace lo mismo
    }
}
