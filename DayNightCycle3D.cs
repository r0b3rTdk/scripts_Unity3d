using UnityEngine;

public class DayNightCycle3D : MonoBehaviour
{
    public Light directionalLight; // Sol (Directional Light)
    public Light[] posteLights; // Luzes dos postes (Point Lights)
    public Color dayColor = Color.white; // Cor do sol durante o dia
    public Color nightColor = new Color(0.1f, 0.1f, 0.3f); // Cor do sol à noite
    public float cycleDuration = 60f; // Duração do ciclo (segundos)

    private float timeElapsed = 0f;

    void Update()
    {
        // Atualiza o tempo no ciclo
        timeElapsed += Time.deltaTime;

        // Progresso do ciclo (0 a 1)
        float cycleProgress = (timeElapsed % cycleDuration) / cycleDuration;

        // Calcula a interpolação entre dia e noite
        float t = Mathf.PingPong(cycleProgress * 2f, 1f);

        // Ajusta a cor e intensidade da luz direcional
        directionalLight.color = Color.Lerp(dayColor, nightColor, t);
        directionalLight.intensity = Mathf.Lerp(1f, 0.2f, t);

        // Ajusta a iluminação global do ambiente
        RenderSettings.ambientLight = Color.Lerp(Color.white, Color.black, t);

        // Rotação do sol (direcional light) para simular movimento
        directionalLight.transform.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(0f, 180f, cycleProgress), 0, 0));

        // Ajusta as luzes dos postes
        AdjustPostLights(t);
    }

    void AdjustPostLights(float t)
    {
        // Ativa os postes apenas à noite
        foreach (Light posteLight in posteLights)
        {
            posteLight.intensity = Mathf.Lerp(0f, 2f, Mathf.Clamp01(t));
        }
    }
}
