using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameUICounters : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI textMeshPro;

    private GameObject playerObject; 
    DynamiteThrower playerScript;
    private GameObject heliObject;
    CharacterController2D heliScript;

    private int currentDynomiteCount;
    private bool dynodudeAlive;
    private int heliHealth;

    // Start is called before the first frame update
    void Start()
    {
        //playerObject = GameObject.FindGameObjectWithTag("DynomiteDude");
        //playerScript = playerObject.GetComponent<DynamiteThrower>();
        //heliObject = GameObject.FindGameObjectWithTag("Player");
        //heliScript = heliObject.GetComponent<CharacterController2D>();
        //textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        //heliHealth = heliScript.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (textMeshPro == null)
        {
            textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        }
        if (playerObject == null)
            if (playerObject == null)
            {
            playerObject = GameObject.FindGameObjectWithTag("DynomiteDude");
            playerScript = playerObject.GetComponent<DynamiteThrower>();
        }
        if (heliObject == null)
        {
            heliObject = GameObject.FindGameObjectWithTag("Player");
            heliScript = heliObject.GetComponent<CharacterController2D>();
        }
        currentDynomiteCount = playerScript.currentDynomiteCount;
        //Debug.Log(currentDynomiteCount);
        dynodudeAlive = playerScript.dudeIsAlive;
        //Debug.Log(dynodudeAlive);
        heliHealth = heliScript.currentHealth;
        //Debug.Log(heliHealth);
        textMeshPro.text = string.Format("dynoMITE: {0}\nthe_guy is alive: {1}\nhelicopter health: {2}", currentDynomiteCount, dynodudeAlive, heliHealth);
    }
}
