using System;  //.net tarih saat kütüphanesi için gerekli
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform hT;    //hours
    public Transform mT;    //minutes
    public Transform sT;    //seconds 
    public bool secenek;

    const float degreesPerHour = 30f;
    const float degreesPerMinute = 6f;
    const float degreesPerSecond = 6f;    

    void Awake()
    {   //görüntü oluşturulmadan önce ekranda ibrelerin doğru konumda olmasını sağlar
        DateTime time = DateTime.Now;
        hT.localRotation = Quaternion.Euler(0f, time.Hour * degreesPerHour, 0f);
        mT.localRotation = Quaternion.Euler(0f, time.Minute * degreesPerMinute, 0f);
        sT.localRotation = Quaternion.Euler(0f, time.Second * degreesPerSecond, 0f);
    }
    void Update()
    {
        if (secenek)
        {
            UpdateContinuous();
        }
        else
        {
            UpdateDiscrete();
        }
    }
    void UpdateContinuous () {
        TimeSpan time = DateTime.Now.TimeOfDay;
        hT.localRotation =  Quaternion.Euler(0f, (float) time.TotalHours * degreesPerHour, 0f);
        mT.localRotation = Quaternion.Euler(0f, (float) time.TotalMinutes * degreesPerMinute, 0f);
        sT.localRotation = Quaternion.Euler(0f, (float) time.TotalSeconds * degreesPerSecond, 0f);		
	}
	void UpdateDiscrete () {
		DateTime time = DateTime.Now;
        hT.localRotation =  Quaternion.Euler(0f, time.Hour * degreesPerHour + time.Minute / 2f, 0f);
        mT.localRotation = Quaternion.Euler(0f, time.Minute * degreesPerMinute + time.Second/10f, 0f);
        sT.localRotation = Quaternion.Euler(0f, time.Second * degreesPerSecond, 0f);
	}
}
