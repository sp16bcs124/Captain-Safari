using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimalRevive : MonoBehaviour
{
    public bool isAnimationOne = true;
    public AnimationClip HealedAnim;
    public AnimationClip DyingAnim;
    public GameObject FadeInOut;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Animation>().clip = DyingAnim;
        GetComponent<Animation>().Play();
    }
    public void StartHealing()
    {
        this.tag = "Untagged";
        Invoke("Healed", 4f);
    }
    public void Healed()
    {
        FadeInOut.SetActive(true);
        FadeInOut.GetComponent<Animation>().Play();
        Invoke("DelayHealed", 1f);
        Invoke("DisableFade", 2f);
    }
    void DelayHealed()
    {
        FindObjectOfType<GameManager>().AnimalsToHeal = FindObjectOfType<GameManager>().AnimalsToHeal - 1;
        if (FindObjectOfType<GameManager>().AnimalsToHeal == 0)
        {
            FindObjectOfType<GameManager>().TaskDone();
        }
        GetComponent<Animation>().CrossFade(HealedAnim.name, .3f);
    }
    void DisableFade()
    {
        FadeInOut.SetActive(false);
        FadeInOut.GetComponent<Animation>().Stop();
    }
  
}
