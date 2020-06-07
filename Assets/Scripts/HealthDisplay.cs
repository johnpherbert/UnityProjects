using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{

    TextMeshProUGUI HealthText;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        HealthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = player.GetHP().ToString();
    }
}
