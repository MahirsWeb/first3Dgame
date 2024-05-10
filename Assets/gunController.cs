using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public int reservedAmmoCapacity = 270;

    //Variables that change throughout code
    private bool _canShoot;
    private int _currentAmmoInClip;
    private int _ammoInReserve;

    ////Muzzleflash
    public Image muzzleFlashImage;
    public Sprite[] flashes;

    ////Weapon Recoil
    public bool randomizeRecoil;
    public Vector2 randomRecoilConstraints;
    //You only need to assign this if randomize recoil is off
    public Vector2[] recoilPattern;

    private void Start()
    {
        _currentAmmoInClip = clipSize;
        _ammoInReserve = reservedAmmoCapacity;
        _canShoot = true;
    }

    private void Update()
    {

        if (Input.GetMouseButton(0) && _canShoot && _currentAmmoInClip > 0)
        {
            _canShoot = false;
            _currentAmmoInClip--;
            StartCoroutine(ShootGun());
        }
        else if (Input.GetKeyDown(KeyCode.R) && _currentAmmoInClip < clipSize && _ammoInReserve > 0)
        {
            int amountNeeded = clipSize - _currentAmmoInClip;

            if (amountNeeded >= _ammoInReserve)
            {
                _currentAmmoInClip += _ammoInReserve;
                _ammoInReserve -= amountNeeded;
            }
            else
            {
                _currentAmmoInClip = clipSize;
                _ammoInReserve -= amountNeeded;
            }
        }
    }
    

    //private void DetermineRecoil()
    //{
    //    transform.localPosition -= Vector3.forward * 0.1f;

    //    if (randomizeRecoil)
    //    {
    //        float xRecoil = Random.Range(-randomRecoilConstraints.x, randomRecoilConstraints.x);
    //        float yRecoil = Random.Range(-randomRecoilConstraints.y, randomRecoilConstraints.y);

    //        Vector2 recoil = new Vector2(xRecoil, yRecoil);

    //        _currentRotation += recoil;
    //    }
    //    else
    //    {
    //        int currentStep = clipSize + 1 - _currentAmmoInClip;
    //        currentStep = Mathf.Clamp(currentStep, 0, recoilPattern.Length - 1);

    //        _currentRotation += recoilPattern[currentStep];
    //    }
    //}

    private IEnumerator ShootGun()
    {
        //DetermineRecoil();
        StartCoroutine(MuzzleFlash());

        RaycastForEnemy();

        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }

    private void RaycastForEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hit, 1 << LayerMask.NameToLayer("Enemy")))
        {
            try
            {
                Debug.Log("Hit an Enemy!");
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;
                rb.AddForce(transform.parent.transform.forward * 500);
            }
            catch
            {

            }
        }
    }

    private IEnumerator MuzzleFlash()
    {
        muzzleFlashImage.sprite = flashes[Random.Range(0, flashes.Length)];
        muzzleFlashImage.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        muzzleFlashImage.sprite = null;
        muzzleFlashImage.color = new Color(0, 0, 0, 0);
    }
}
