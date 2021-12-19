const float minus_infinity = -(1. / 0.);
const float infinity = (1. / 0.);
const float PI = 3.14159265359;
const float E = 2.7182818284590452353;

float logWithBase(float base, float x)
{
    return log(x) / log(base);
}

float Normalize(float value, float min, float max)
{
    return clamp((value - min) / (max - min), 0, 1);
}

float OneMinus(float value)
{
    return 1 - value;
}


float Average2(float value[2])
{
    float sum = 0;
    for (int i = 0; i < 2; i++)
    {
        sum += value[i];

    }
    return sum / 2;
}

float CosineCurve(float x, float steepness, float offset)
{
  
    return clamp(1 - cos(x * PI * steepness) + offset, 0, 1);

}

 
float ExponentialCurve(float x, float exponent, float offset)
{
    return clamp(1 - ((1 - pow(x, exponent)) / 1) + offset, 0, 1);
}

 
float LinearCurve(float x, float slope, float offset)
{
    return clamp((x / slope) - offset, 0, 1);
}

 
float LogisticCurve(float x, float k, float x0)
{
    float expPow = -k * ((4 * (E) * (x - x0)) - (2 * E));
    return clamp(1 / (1 + pow(E, expPow)), 0, 1);
}

float LogitCurve(float x, float logBase)
{
    
    return clamp((logWithBase(x / (1 - x), logBase) + (2 * E)) / (4 * E), 0, 1);
}

 
float SineCurve(float x, float steepness, float offset)
{
    return clamp(sin(x * PI * steepness) + offset, 0, 1);
}

static float SmoothstepCurve(float x)
{
    return clamp(x * x * (3 - 2 * x), 0, 1);
}

static float SmootherstepCurve(float x)
{
    return clamp(x * x * x * (x * (6 * x - 15) + 10), 0, 1);
}