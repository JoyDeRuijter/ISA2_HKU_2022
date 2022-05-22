// Written by Joy de Ruijter
using UnityEngine;

public class WeaponHolster : MonoBehaviour
{
    #region Variables

    [SerializeField] private int capacity = 2;
    [SerializeField] private Weapon[] weapons;

    public bool isHolstered { get; private set; }

    private int selectedWeapon = 0;

    private Weapon currentWeapon;
    private GameObject prefabCurrentWeapon;
    private GameObject goCurrentWeapon;
    private GameObject[] goWeapons;

    #endregion

    private void Awake()
    {
        //currentWeapon = weapons[selectedWeapon];
        //prefabCurrentWeapon = currentWeapon.weaponPrefab;
        //goWeapons = new GameObject[weapons.Length];        
    }

    private void Start()
    {
        //InstantiateWeapons();
        //SelectWeapon();
    }

    private void Update()
    {
        /*
        Mathf.Clamp(weapons.Length, 0, capacity);

        if (!isHolstered)
        {
            int previousSelectedWeapon = selectedWeapon;
            HandleInput();

            if (previousSelectedWeapon != selectedWeapon)
                SelectWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Q))
            HolsterWeapon(selectedWeapon);
        */
    }

    private void InstantiateWeapons()
    {
        for(int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].weaponPrefab == null)
            {
                Debug.Log(weapons[i].name + " has no weaponprefab attached");
                return;
            }
            GameObject instantiateObject = Instantiate(weapons[i].weaponPrefab, transform);
            instantiateObject.transform.localPosition = weapons[i].spawnPosition;
            instantiateObject.transform.localRotation = weapons[i].spawnRotation;
            goWeapons[i] = instantiateObject;
            //goWeapons[i].SetActive(false);
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Weapon weapon in weapons)
        {
            if (i == selectedWeapon)
            {
                goCurrentWeapon = goWeapons[i];
                currentWeapon = weapon;
                goWeapons[i].SetActive(true);
            }
            else
                goWeapons[i].SetActive(false);

            i++;
        }
    }

    private void HandleInput()
    {
        // Scrollwheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // Up
        {
            if (selectedWeapon >= weapons.Length - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // Down
        {
            if (selectedWeapon <= 0)
                selectedWeapon = weapons.Length - 1;
            else
                selectedWeapon--;
        }

        // Numberkeys
        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedWeapon = 0;

        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Length >= 2)
            selectedWeapon = 1;

        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Length >= 3)
            selectedWeapon = 2;

        if (Input.GetKeyDown(KeyCode.Alpha4) && weapons.Length >= 4)
            selectedWeapon = 3;
    }

    private void HolsterWeapon(int selectedWeapon)
    {
        if (isHolstered)
        {
            isHolstered = false;
            // re-enable shooting
            goWeapons[selectedWeapon].SetActive(true);
        }

        else if (!isHolstered)
        {
            isHolstered = true;
            // disable shooting
            goWeapons[selectedWeapon].SetActive(false);
        }
    }
}
