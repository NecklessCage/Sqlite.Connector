namespace Rdlc.Generator
{
    using System.Xml.Linq;

    public interface IElement
    {
        XElement Element { get; }
    }
}
