using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class PredictTester : MonoBehaviour {

    NGram<KeyCode> ngram;

    void Start()
    {
        ngram = new NGram<KeyCode>(3);
    }

    void Update()
    {
    }
}
