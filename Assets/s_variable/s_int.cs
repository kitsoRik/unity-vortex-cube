using System.Linq;


public class s_int : s_code
{
    private string save_int;

    public string INT
    {
        get
        {
            return Decode(save_int);
        }

        set
        {
            save_int = Encode(value);
        }
    }

    public override string ToString()
    {
        return INT.Length == 0 ? "0" : INT;
    }

    public static implicit operator int(s_int _s_int) // use as int
    {
        if (_s_int == null || _s_int.INT.Length == 0)
            return 0;
        return int.Parse(_s_int.INT);
    }

    public static implicit operator s_int(int _int) // just parse int to save int
    {
        s_int _s_int = new s_int();
        _s_int.INT = _int.ToString();
        return _s_int;
    }

    public static implicit operator string(s_int _s_int) // use as int
    {
        if (_s_int == null || _s_int.INT.Length == 0)
            return "0";
        return _s_int.INT;
    }

    public static implicit operator s_int(string _int) // just parse int to save int
    {
        s_int _s_int = new s_int();
        if (_int.Any((c) => !char.IsDigit(c)))
        {
            throw new System.Exception("String is not a int number");
        }
        _s_int.INT = _int;
        return _s_int;
    }

}
