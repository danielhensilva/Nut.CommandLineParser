namespace Nut.CommandLineParser.Specialized
{
    internal interface ISpecializedParser<out TElement>
    {
        TElement Parse(string args);
    }
}