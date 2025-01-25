using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PowerUpsUI : MonoBehaviour
{
    [SerializeField] private Button lidButton;
    [SerializeField] private Button popAllButton;
    [SerializeField] private Button lowerLevelButton;

    [SerializeField] private List<Sprite> lidButtonSpries;
    [SerializeField] private List<Sprite> popAllButtonSprites;
    [SerializeField] private List<Sprite> lowerLevelButtonSprites;

    [SerializeField] private TextMeshProUGUI lidPowerUpCounter;
    [SerializeField] private TextMeshProUGUI popAllPowerUpCounter;
    [SerializeField] private TextMeshProUGUI lowerLevelButtonCounter;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
