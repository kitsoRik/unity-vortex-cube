using System.Linq;


public class s_float : s_code
{
    private string save_float;

    public string FLOAT
    {
        get
        {
            return Decode(save_float);
        }

        set
        {
            save_float = Encode(value);
        }
    }

    public override string ToString()
    {
        return FLOAT.Length == 0 ? "0" : FLOAT;
    }

    public static implicit operator float(s_float _s_float) // use as float
    {
        if (_s_float == null || _s_float.FLOAT.Length == 0)
            return 0;
        return float.Parse(_s_float.FLOAT);
    }

    public static implicit operator s_float(float _float) // just parse float to save float
    {
        s_float _s_float = new s_float();
        _s_float.FLOAT = _float.ToString();
        return _s_float;
    }

    public static implicit operator string(s_float _s_float) // use as float
    {
        if (_s_float == null || _s_float.FLOAT.Length == 0)
            return "0";
        return _s_float.FLOAT;
    }

    public static implicit operator s_float(string _float) // just parse float to save float
    {
        s_float _s_float = new s_float();
        if (_float.Any((c) => !char.IsDigit(c)))
        {
            throw new System.Exception("String is not a float number");
        }
        _s_float.FLOAT = _float;
        return _s_float;
    }

}
