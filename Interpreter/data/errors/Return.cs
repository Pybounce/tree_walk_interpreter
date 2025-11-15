

public class Return: Exception
{
    public readonly Object val;

    public Return(Object val): base(null, null)
    {
        this.val = val;
    }
}