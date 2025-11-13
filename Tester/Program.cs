using SerialSoup.Attributes;
using SerialSoup.Tokens;

TestObject.Test();

public class Wrapper : TokenizableObject
{
    [TokenizeField]
    private int x;
    [TokenizeField]
    private float y;
    [TokenizeField]
    private string z;

    public Wrapper(int x=0, float y=0f, string z="")
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

public class TestObject : TokenizableObject
{
    [TokenizeField]
    private Wrapper value;

    public TestObject(int x=0, float y=0, string z="")
    {
        value = new Wrapper(x,y,z);
    }

    public static void Test()
    {
        TestObject a = new TestObject(3, 2, "aaa");
        TestObject b = new TestObject();
        b.Detokenize(a.Tokenize());
        Console.WriteLine(b);
    }
}