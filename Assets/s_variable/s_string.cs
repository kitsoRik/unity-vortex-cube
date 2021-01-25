using System.Linq;


public class s_string : s_code
{
    private string save_string;

    public string STRING
    {
        get
        {
            return Decode(save_string);
        }

        set
        {
            save_string = Encode(value);
        }
    }

    public static implicit operator string(s_string _s_string) // use as string
    {
        return _s_string.STRING;
    }

    public static implicit operator s_string(string _string) // just parse string to save string
    {
        s_string _s_string = new s_string();
        _s_string.STRING = _string;
        return _s_string;
    }

}
