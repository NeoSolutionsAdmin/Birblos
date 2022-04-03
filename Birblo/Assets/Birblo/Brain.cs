using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{

    //Models
    public GameObject BirbloModel;

    //sensors
    public EyeSensor Smell;
    public Muscle Muscle;
    public Life MyLife;
    public BodyStats bodyStats;

    public List<float> SmellDirectionHistory = new List<float>();
    

    public enum Origin { Son, New };
    public Origin MyORIGIN = Origin.New;

    //topology
    public List<int> topology = new() { 2, 5, 5, 3 };
    public List<float[,]> weights = new();
    public List<float> biases = new();
    public int Gen = 0;

    //in and out
    public float[] IN;
    public float[] OUT;

    //mutation ratio
    public float murationRatio = 1f;

    //parametes
    public int NumberOfSons = 2;
    

    // Start is called before the first frame update
    void Start()
    {
        float columnas;
        float filas;
        if (MyORIGIN == Origin.New) 
        {
                for (int layer = 1; layer < topology.Count; layer++)
                {
                    columnas = topology[layer - 1];
                    filas = topology[layer];


                    float[,] layer_weights = new float[topology[layer - 1], topology[layer]];
                    biases.Add(Random.Range(-10, 10));


                    for (int j = 0; j < columnas; j++)
                    {
                        for (int k = 0; k < filas; k++)
                        {
                            layer_weights[j, k] = Random.Range(-10, 10);
                        }


                    }
                    weights.Add(layer_weights);
                }
            
        }
    }

    public List<float[,]> GetWeights()
    {
        return new List<float[,]>(weights);
    }

    public List<float> GetBiases()
    {
        return new List<float>(biases);
    }

    public List<int> GetTopology()
    {
        return new List<int>(topology);
    }


    public void StartSon(List<int> T, List<float[,]> W, List<float> B, int Generation)
    {

        MyORIGIN =  Origin.Son;
        topology = new List<int>(T);
        weights = new List<float[,]>(W);
        biases = new List<float>(B);
        Gen = Generation;
        Gen++;
        gameObject.transform.parent.name = "Birblo Generation: " + Gen++;

        int ChangeGrade = Random.Range(1, 10);
        for (int a = 0; a < ChangeGrade; a++)
        {

            int randomLayer = Random.Range(0, topology.Count - 1);
            int filas = weights[randomLayer].GetLength(0);
            int columnas = weights[randomLayer].GetLength(1);

            int RandomFila = Random.Range(0, filas);
            int RandomColumna = Random.Range(0, columnas);

            float value = weights[randomLayer][RandomFila, RandomColumna];

            value += Random.Range(-1f, 1f) * value * murationRatio;
            weights[randomLayer][RandomFila, RandomColumna] = value;
        }



    }

    public float[] ProcessNetwork(float[] Input)
    {
        float[] zl = Input;
        float columnas;
        float filas;



        for (int layer = 1; layer < topology.Count; layer++)
        {

            float[] zlnew = new float[topology[layer]];

            columnas = topology[layer - 1];
            filas = topology[layer];

            for (int fila = 0; fila < filas; fila++)
            {
                for (int columna = 0; columna < columnas; columna++)
                {
                    try
                    {
                        zlnew[fila] += weights[layer - 1][columna, fila] * zl[columna];
                    }
                    catch (System.Exception exc)
                    {
                        Debug.LogWarning(exc);
                    }


                }

                zlnew[fila] += biases[layer - 1];

                if (layer < topology.Count - 1) { zlnew[fila] = zlnew[fila] > 0 ? zlnew[fila] : 0.01f * zlnew[fila]; }
                else

                {
                    decimal r1 = new decimal(-zlnew[fila]);
                    double d1 = (double)r1;
                    zlnew[fila] = (float)(1 / (1 + System.Math.Exp(d1)));
                };

            }
            zl = zlnew;
        }

        return zl;
    }

    private void FixedUpdate()
    {
        IN = new float[] { Smell.Distance / Smell.MaxDistanceSensor, Smell.Direction / 360f };
        float[] result = ProcessNetwork(IN);

        SmellDirectionHistory.Add(Smell.Direction / 360f);
        if (SmellDirectionHistory.Count > 20)
        {
            SmellDirectionHistory.RemoveAt(0);
        }
        

        OUT = result;
        Muscle.Right = result[0] > 0.1f ? true : false;
        Muscle.Left = result[1] > 0.1f ? true : false;
        Muscle.Fwd = result[2] > 0.1f ? true : false;
        Muscle.Bck = result[3] > 0.1f ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Smell.EAT == true) 
        {
            Smell.HIT = false;
            

            MyLife.AddLife(2000);

            for (int a = 0; a < NumberOfSons; a++) {

                GameObject B = Instantiate(BirbloModel);
                B.transform.position = this.transform.parent.position;
                B.transform.Rotate(Vector3.up * (Random.Range(-180f, 180f)));
                Brain BabyBrain = B.transform.Find("Brain").GetComponent<Brain>();
                BabyBrain.StartSon(topology, weights, biases, Gen);
                BabyBrain.MyLife.AddLife(100);
            }
        }


    }
}
