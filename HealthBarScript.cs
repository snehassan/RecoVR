using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HealthBarScript : MonoBehaviour
{
    private EnemyScript es;
    private float health;
    private Slider slider;
    private CanvasGroup sliderCG;
    // Start is called before the first frame update
    void Start()
    {
        es = transform.root.GetComponent<EnemyScript>();
        slider = GetComponent<Slider>();
        sliderCG = slider.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        health = es.health;

        slider.value = health * .01f;

        transform.LookAt(es.Target.position);

        if (Vector3.Distance(transform.position, es.Target.position) < 3 && !es.dead)
            sliderCG.DOFade(1, 0.5f);
        else
            sliderCG.DOFade(0,.5f);

    }
}
