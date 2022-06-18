// Written by Joy de Ruijter
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class ThirdPersonShooterController : MonoBehaviour
{
    #region Variables

    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private Transform bulletSpawnPosition;
    [SerializeField] private Transform vfxHitHuman;
    [SerializeField] private Transform vfxHitTerrain;
    [SerializeField] private Transform aimObject;
    [SerializeField] private MultiAimConstraint bodyAim;
    [SerializeField] private MultiAimConstraint aim;
    [SerializeField] private TwoBoneIKConstraint secondHand;
    [SerializeField] private ParticleSystem muzzleFlash;

    private List<GameObject> hitVFXs = new List<GameObject>();
    private Transform lastHitVFX;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetInputs;
    private Animator anim;

    [HideInInspector] public bool shoot;

    #endregion

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetInputs = GetComponent<StarterAssetsInputs>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    { 
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
            aimObject.transform.position = mouseWorldPosition;
        }

        if (starterAssetInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            bodyAim.gameObject.SetActive(true);
            aim.gameObject.SetActive(true);
            secondHand.gameObject.SetActive(true);
            bodyAim.weight = Mathf.Lerp(bodyAim.weight, 0.7f, Time.deltaTime * 10f);
            aim.weight = Mathf.Lerp(bodyAim.weight, 1f, Time.deltaTime * 10f);
            secondHand.weight = 1f;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        { 
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetRotateOnMove(true);
            thirdPersonController.SetSensitivity(normalSensitivity);
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 0f, Time.deltaTime * 10f));

            bodyAim.weight = Mathf.Lerp(bodyAim.weight, 0f, Time.deltaTime * 10f);
            aim.weight = Mathf.Lerp(bodyAim.weight, 0f, Time.deltaTime * 10f);
            secondHand.weight = 0f;
            bodyAim.gameObject.SetActive(false);
            aim.gameObject.SetActive(false);
            secondHand.gameObject.SetActive(false);
        }

        if (starterAssetInputs.shoot)
        {
            if (hitTransform != null)
            {
                shoot = true;
                muzzleFlash.Play();
                if (hitTransform.GetComponent<BulletTarget>() != null)
                {
                    lastHitVFX = Instantiate(vfxHitHuman, mouseWorldPosition, Quaternion.identity);
                    hitVFXs.Add(lastHitVFX.gameObject);
                }
                else
                {
                    lastHitVFX = Instantiate(vfxHitTerrain, mouseWorldPosition, Quaternion.identity);
                    hitVFXs.Add(lastHitVFX.gameObject);
                }
                DestroyHitVFXs();
            }

            //Projectile spawning
            //Vector3 aimDir = (mouseWorldPosition - bulletSpawnPosition.position).normalized;
            //Instantiate(bulletPrefab, bulletSpawnPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));

            starterAssetInputs.shoot = false;
        }
        else
            shoot = false;
    }

    private void DestroyHitVFXs()
    {
        if (hitVFXs.Count >= 5)
        {
            for (int i = 0; i <= hitVFXs.Count - 3; i++)
            {
                Destroy(hitVFXs[i]);
            }
        }
    }
}
