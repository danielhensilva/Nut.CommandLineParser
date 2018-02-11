# Nut.CommandLineParser
[![Build status](https://ci.appveyor.com/api/projects/status/jhxbfjarb9qbpsep/branch/master?svg=true)](https://ci.appveyor.com/project/danielhensilva/nut-commandlineparser/branch/master)

<br/>

##### Parse to KeyValuePairs

Just import `Nut.CommandLineParser` namespaces:
```c#
usign Nut.CommandLineParser
using Nut.CommandLineParser.Attributes;
using Nut.CommandLineParser.Exceptions;
```

And call `new Parser().ParseToKeyValuePairs(args)`, where `args` is any string or array of strings to be parsed into a collection of KeyValues. 

Arguments can be defined in 4 different ways:

* Key separated by equals sign: `" key=value "`
* Key (alias) prefixed with one slash: `" -k value "`
* Key prefixed with two slashs: `" --key value "`
* Key with null value: `" key "`

Any value containing spaces or special characters must be wrapped by double quotes `"`

`Key=\"Foo bar\"`

Also notice key-alias only support one char long:

* Valid (alias with one char): `-c value`
* Invalid (alias with more than one char): `-command value`

##### Samples

```c#
void App()
{
    var args = "primary=blue secondary=\"purple and red\" --optional yes";
    var output = new Parser().ParseToKeyValuePairs(args);

    foreach (var item in output)
        Console.WriteLine("{0} -> {1}", item.Key, item.Value ?? "(null)");

    // Output:
    //
    // ChangeColor -> (null)
    // primary -> blue
    // secondary -> purple and red
    // optional -> yes
}
```
```c#
void App()
{
    var args = "first=Alice last=Souza --fullname \"Alice Souza\" -a \"AS\"";
    var output = new Parser().ParseToKeyValuePairs(args);

    foreach (var item in output)
        Console.WriteLine("{0} -> {1}", item.Key, item.Value ?? "(null)");
    
    // Output:
    //
    // first -> Alice
    // last -> Souza
    // fullname -> Alice Souza
    // a -> AS
}
```

<br/>

##### Parse to Object

It is also possible to parse directly to any class.

Just call `new Parser().ParseToObject<TType>(args)`, where `args` is any string or array of strings to be parsed into specified `TType`. 

Two rules need to be followed:

* Properties must be decorated with `[OptionAlias]` and/or `[OptionName]` attributes. It can have any number of these attributes per property as long as aliases and names are unique for that type.
* Properties types must be primitives, decimal or string. No complex type is supported yet.

##### Samples

```c#
class MyBook()
{
    [OptionAlias('i')]
    [OptionAlias('k')]
    [OptionName("id")]
    public int BookId { get; set; }
    
    [OptionAlias('n')]
    [OptionName("name")]
    public string BookName { get; set; }
    
    [OptionAlias('q')]
    [OptionName("qtd")]
    public long StockQtd { get; set; }

    [OptionAlias('v')]
    [OptionName("value")]
    [OptionName("pricing")]
    public decimal StockPrice { get; set; }
}
```
```c#
void App()
{
    var args = "-i 19 -n \"A duck story\" -q 4 -v 14.99";
    var output = new Parser().ParseToObject<MyBook>(args);

    Console.WriteLine("BookId -> {0}", output.BookId);
    Console.WriteLine("BookName -> {0}", output.BookName);
    Console.WriteLine("StockQtd -> {0}", output.StockQtd);
    Console.WriteLine("StockPrice -> {0}", output.StockPrice);
    
    // Output:
    //
    // BookId -> 19
    // BookName -> A duck story
    // StockQtd -> 4
    // StockPrice -> 14.99
}
```

##### Exceptions

* Tokens not found in given type:
```c#
void App()
{
    try
    {
        var args = "-z 10";
        new Parser().ParseToObject<MyBook>(args);
    }
    catch (UnboundTokenException ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine("Token: {0}", ex.Token);
    }
    
    // Output:
    //
    // Unbound token z.
    // Token: z
}

```

* Unexpected character during parse:

```c#
void App()
{
    try
    {
        var args = "id=19 name=\"A duck story\" qtd~4 value=14.99";
        new Parser().ParseToObject<MyBook>(args);
    }
    catch (UnexpectedTokenException ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine("Token: {0}", ex.Token);
        Console.WriteLine("Index: {0}", ex.Index);
    }
    
    // Output:
    //
    // Unexpected token ~4 at index 29.
    // Token: ~4
    // Index: 29
}
```

* Attribute mapping errors:

```c#
class Storage
{
    [OptionName("path")]
    public string Path { get; set; }

    [OptionName("path")]
    [OptionName("filename")]
    public string FileName { get; set; }

    [OptionName("filename")]
    public string Extension { get; set; }
}
```

```c# 
void App()
{
    try
    {
        var args = "filename=sample.txt";
        new Parser().ParseToObject<Storage>(args);
    }
    catch (DuplicatedOptionsException ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine("Tokens: {0}", string.Join("|", ex.Duplications));
    }
    
    // Output:
    //
    // Duplicated keys are found: [path, filename].
    // Tokens: path|filename
}
```