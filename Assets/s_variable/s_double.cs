using System.Linq;


public class s_double : s_code
{
    private string save_double;

    public string DOUBLE
    {
        get
        {
            return Decode(save_double);
        }

        set
        {
            save_double = Encode(value);
        }
    }

    public override string ToString()
    {
        return DOUBLE.Length == 0 ? "0" : DOUBLE;
    }

    public static implicit operator double(s_double _s_double) // use as double
    {
        if (_s_double == null || _s_double.DOUBLE.Length == 0)
            return 0;
        return double.Parse(_s_double.DOUBLE);
    }

    public static implicit operator s_double(double _double) // just parse double to save double
    {
        s_double _s_double = new s_double();
        _s_double.DOUBLE = _double.ToString();
        return _s_double;
    }

    public static implicit operator string(s_double _s_double) // use as double
    {
        if (_s_double == null || _s_double.DOUBLE.Length == 0)
            return "0";
        return _s_double.DOUBLE;
    }

    public static implicit operator s_double(string _double) // just parse double to save double
    {
        s_double _s_double = new s_double();
        if (_double.Any((c) => !char.IsDigit(c)))
        {
            throw new System.Exception("String is not a double number");
        }
        _s_double.DOUBLE = _double;
        return _s_double;
    }

}
