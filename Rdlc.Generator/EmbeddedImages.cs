namespace Rdlc.Generator
{
    public class EmbeddedImages : CollectionOf<EmbeddedImage>, IElement
    {
        protected sealed override string GetRdlName()
        {
            return typeof(EmbeddedImages).GetShortName();
        }
    }
}
