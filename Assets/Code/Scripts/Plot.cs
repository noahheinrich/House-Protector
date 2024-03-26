using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer sr; 
    [SerializeField] private Color hoverColor;

    private GameObject towerObj;
    public Turret turret;
    private Color startColor;

    
    // Start is called before the first frame update
    void Start()
    {
        startColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUI()) {
            return;
        }

        if (towerObj != null) {
            turret.OpenUpgradeUI();
            return;
        }

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.main.currency) {
            Debug.Log("You can't afford this tower");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);

        towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = towerObj.GetComponent<Turret>();
    }
}
