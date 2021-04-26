using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameUICounters : MonoBehaviour
{
	public TMPro.TMP_Text test;

    DynamiteThrower playerScript;
    CharacterController2D heliScript;

    private int currentDynomiteCount;
    private bool dynodudeAlive;
    private int heliHealth;

    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        //textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
		
        if (playerScript == null)
		{
            playerScript = GameObject.FindGameObjectWithTag("DynomiteDude").GetComponent<DynamiteThrower>();
        }
        if (heliScript == null)
        {
            heliScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
        }
		
        currentDynomiteCount = playerScript.currentDynomiteCount;
        dynodudeAlive = playerScript.dudeIsAlive;
        heliHealth = heliScript.currentHealth;
		
		test.text = string.Format("dynoMITE: {0}\nthe_guy is alive: {1}\nhelicopter health: {2}", currentDynomiteCount, dynodudeAlive, heliHealth);
    }
}
