using UnityEngine;
using TMPro;

public class Calculator : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Using a temp decimal for accuacy 
    decimal prevInput;

    bool clearPrevInput;
    private EquationType equationType;

    private void Start()
    {
        Clear();
    }

    #region EXTRA
    // To change a number from pos to neg and vice versa we multiply by -1
    public void NegAndPos()
    {
        text.text = (decimal.Parse(text.text) * (-1)).ToString();
    }

    // To get the percent we divide what is in the solution box by 100
    public void Percent()
    {
        text.text = (decimal.Parse(text.text) / 100).ToString();
    }

    // Using the Sin, Cos, Tan Function on what is on the current value in the text box
    public void Sin()
    {
        text.text = (Mathf.Sin((float.Parse(text.text)))).ToString();
    }
    public void Cos()
    {
        text.text = (Mathf.Cos((float.Parse(text.text)))).ToString();
    }
    public void Tan()
    {
        text.text = (Mathf.Tan((float.Parse(text.text)))).ToString();
    }
    #endregion

    #region SET_EQUATIONS
    // We wasnt to set the equations when the user clicks the buttons
    // EXTRA: pull these out to a simplified func
    public void SetEquationAsAdd()
    {
        prevInput = decimal.Parse(text.text);
        clearPrevInput = true;
        equationType = EquationType.ADD;
    }

    public void SetEquationsAsSub()
    {
        prevInput = decimal.Parse(text.text); 
        clearPrevInput = true;
        equationType = EquationType.SUBTRACT;
    }

    public void SetEquationsAsMult()
    {
        prevInput = decimal.Parse(text.text);
        clearPrevInput = true;
        equationType = EquationType.MULTIPLY;
    }

    public void SetEquationAsDiv()
    {
        prevInput = decimal.Parse(text.text);
        clearPrevInput = true;
        equationType = EquationType.DIVIDE;
    }
    #endregion

    #region PRIVATE
    // These are private because no other scripts will need access
    void Add()
    {
        decimal n1 = decimal.Parse(text.text);
        decimal solution = n1 +  prevInput;
        text.text = solution.ToString();
    }
    void Subtract()
    {
        decimal n1 = decimal.Parse(text.text);
        decimal solution = prevInput - n1;
        text.text = solution.ToString();
    }
    void Multiply()
    {
        decimal n1 = decimal.Parse(text.text);
        decimal solution = prevInput * n1;
        text.text = solution.ToString();
    }
    void Divide()
    {
        decimal n1 = decimal.Parse(text.text);
        decimal solution = prevInput / n1;
        text.text = solution.ToString();
    }
    #endregion

    public void AddInput(string input)
    {
        // Check if we want to remove the prev input as well as checking for 0
        if (clearPrevInput)
        {
            text.text = string.Empty;
        }
        clearPrevInput = false;
        // we check the if the only value is 0 and replace rather than add onto
        if (text.text == "0")
        {
            text.text = input;
        }
        else
        {
            text.text += input;
        }
    }

    public void Clear()
    {
        text.text = "0";
        prevInput = 0;

        //TODO: Leave this alone
        equationType = EquationType.None;        
    }

    public void Calculate()
    {
        // When the user clickes the '=' button we check what the equation type is and run the corisponding func.
        if (equationType == EquationType.ADD) Add();
        if (equationType == EquationType.MULTIPLY) Multiply();
        if (equationType == EquationType.SUBTRACT) Subtract();
        if (equationType == EquationType.DIVIDE) Divide();
    }

    public enum EquationType
    {
        None = 0,
        ADD = 1,
        SUBTRACT = 2,
        MULTIPLY = 3,
        DIVIDE = 4
    }
}
