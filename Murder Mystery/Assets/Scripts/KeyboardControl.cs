using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardControl : MonoBehaviour
{
    public float verticalSpeed;
    public float rotationSpeed;

    public GameObject bullet;
    public float reloadTime;
    private float reloadTimeTimer = 0;

    private AmmoCollection ammo_collection;
    private HealthDisplay health_display;
    public Text ammoDisplay;

    string leftButton;
    string rightButton;

    float leftButtonHoldTimer = 0;
    float rightButtonHoldTimer = 0;

    public float buttonTapToAttackTime;
    bool areButtonsJustPressed = false;
    bool areButtonsJustPressedAndReleased = false;
    float buttonTapToAttackTimer;
    bool isAttacking = false;

    public GameObject sweepAttack;

    public bool isInvincible;

    bool reversedControls;  // Put if statement with reversedControls at beginning of Update instead of other controls? leftButtonHoldTimer and rightButtonHoldTimer?

    public Image statusImage;
    public Text statusText;
    public float deathColorAlpha;

    public bool enabledSignalFlares;
    public bool isSignalFlareColorWhite;
    public float signalFlareRotationTime;
    public float signalFlareDuration;
    float signalFlareDurationTimer = 0;
    Color originalColor;

    Renderer rend;
    Renderer childRend;

    RoleSelection mainCameraComponent;

    void Start()
    {
        ammo_collection = GetComponent<AmmoCollection>();

        leftButton = "Left" + transform.name.Replace("Player ", "");
        rightButton = "Right" + transform.name.Replace("Player ", "");
        // Debug.Log(transform.name);

        buttonTapToAttackTimer = buttonTapToAttackTime;  // Use separate variable?

        originalColor = GetComponent<SpriteRenderer>().color;
        rend = GetComponent<Renderer>();
        childRend = transform.GetChild(0).gameObject.GetComponent<Renderer>();

        mainCameraComponent = Camera.main.GetComponent<RoleSelection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RoleSelection.isPregame && gameObject.tag == "Murderer")
        {
            reversedControls = true;
        } else  // Apply only for (!RoleSelection.isPregame && gameObject.tag == "Murderer")?
        {
            reversedControls = false;
        }

        if (!RoleSelection.isGameOver)
        {
            if ((Input.GetButton(leftButton) && !reversedControls) || (Input.GetButton(rightButton) && reversedControls))
            {
                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
                leftButtonHoldTimer += Time.deltaTime;
            }
            else
            {
                /*
                if (leftButtonHoldTimer > 0)
                {
                    Debug.Log("Left: " + leftButtonHoldTimer);
                }
                */
                leftButtonHoldTimer = 0;
            }
            if ((Input.GetButton(rightButton) && !reversedControls) || (Input.GetButton(leftButton) && reversedControls))
            {
                transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
                rightButtonHoldTimer += Time.deltaTime;
            }
            else
            {
                /*
                if (rightButtonHoldTimer > 0)
                {
                    Debug.Log("Right: " + rightButtonHoldTimer);
                }
                */
                rightButtonHoldTimer = 0;
            }

            if (Input.GetButton(leftButton) && Input.GetButton(rightButton) && reloadTimeTimer <= 0)  // Combine with above?  // Don't move forward when attacking?
            {
                transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime);  // maybe smooth out movement?  <-- From previous project
                if (leftButtonHoldTimer < buttonTapToAttackTime && rightButtonHoldTimer < buttonTapToAttackTime && !areButtonsJustPressed)
                {
                    areButtonsJustPressed = true;
                }
            }
            if (areButtonsJustPressed)  // BUG/FEATURE: can't move forward in attack state
            {
                buttonTapToAttackTimer -= Time.deltaTime;
                // Debug.Log(buttonTapToAttackTimer);

                if (!Input.GetButton(leftButton) && !Input.GetButton(rightButton) && !isAttacking)
                {
                    areButtonsJustPressedAndReleased = true;
                    areButtonsJustPressed = false;
                    buttonTapToAttackTimer = buttonTapToAttackTime;

                    // WORKING ON DOUBLE-TAP SYSTEM
                    /*
                    isAttacking = true;
                    // Debug.Log("ATTACK");
                    GameObject sweepAttackGO = Instantiate(sweepAttack, transform.position, transform.rotation);
                    sweepAttackGO.name = "Sweep Attack " + gameObject.name.Replace("Player ", "");
                    Physics2D.IgnoreCollision(sweepAttackGO.GetComponent<Collider2D>(), GetComponent<Collider2D>());

                    reloadTimeTimer = reloadTime;
                    */
                }

                if (buttonTapToAttackTimer < 0)
                {
                    areButtonsJustPressed = false;
                    buttonTapToAttackTimer = buttonTapToAttackTime;
                    isAttacking = false;  // Place this somewhere else?
                }
            }
            if (areButtonsJustPressedAndReleased)
            {
                buttonTapToAttackTimer -= Time.deltaTime;

                if (Input.GetButton(leftButton) && Input.GetButton(rightButton) && !isAttacking)
                {
                    isAttacking = true;
                    // Debug.Log("ATTACK");
                    GameObject sweepAttackGO = Instantiate(sweepAttack, transform.position, transform.rotation);
                    sweepAttackGO.name = "Sweep Attack " + gameObject.name.Replace("Player ", "");
                    Physics2D.IgnoreCollision(sweepAttackGO.GetComponent<Collider2D>(), GetComponent<Collider2D>());

                    reloadTimeTimer = reloadTime;
                }

                if (buttonTapToAttackTimer < 0)
                {
                    areButtonsJustPressedAndReleased = false;
                    buttonTapToAttackTimer = buttonTapToAttackTime;
                    isAttacking = false;  // Place this somewhere else?
                }
            }

            if ((leftButtonHoldTimer > signalFlareRotationTime || rightButtonHoldTimer > signalFlareRotationTime) && signalFlareDurationTimer <= 0 && enabledSignalFlares)  // Modify so that when holding other button doesn't affect?
            {
                signalFlareDurationTimer = signalFlareRotationTime;
            }
            if (signalFlareDurationTimer > 0)
            {
                // Debug.Log("In if statement" + gameObject.name);
                Shader defaultShader = Shader.Find("Sprites/Default");

                if (isSignalFlareColorWhite)
                {
                    GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                }

                rend.material.shader = defaultShader;
                childRend.material.shader = defaultShader;

                signalFlareDurationTimer -= Time.deltaTime;
            }
            else
            {
                // Debug.Log("In else statement" + gameObject.name);
                // Debug.Log("Child " + transform.name.Replace("Player ", "") + ": " + GetComponentInChildren<Transform>());
                Shader diffuseShader = Shader.Find("Sprites/Diffuse");

                if (isSignalFlareColorWhite)
                {
                    GetComponent<SpriteRenderer>().color = originalColor;
                }

                rend.material.shader = diffuseShader;
                childRend.material.shader = diffuseShader;
            }

            reloadTimeTimer -= Time.deltaTime;
        }

        /*
        if (Input.GetButton("Fire1") && reloadTimeTimer <= 0 && ammo_collection.ammoCount > 0)
        {
            Instantiate(bullet, transform.position - transform.right, transform.rotation);
            ammo_collection.ammoCount -= 1;
            ammoDisplay.text = "AMMO: " + ammo_collection.ammoCount;
            reloadTimeTimer = reloadTime;
        }
        reloadTimeTimer -= Time.deltaTime;
        */

        /*
        if (health_display.health <= 0)
        {
            Destroy(gameObject);
        }
        */
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.tag == "Bullet" && !isInvincible)
        {
            Destroy(gameObject);
            Color tempColor = statusImage.color;
            tempColor.a = deathColorAlpha;
            statusImage.color = tempColor;

            if (gameObject.tag == "Murderer")
            {
                if (mainCameraComponent.displayMurdererOnDeath)
                {
                    statusText.text = "M";
                }
                mainCameraComponent.murdererTotal--;
            } else if (gameObject.tag == "Innocent"){
                mainCameraComponent.innocentTotal--;
            }
        }
    }
}