namespace stellar_dotnet_sdk.responses.page
{
    /// <summary>
    ///     Links connected to page response.
    /// </summary>
    public class PageLinks
    {
        public PageLinks(Link next, Link prev, Link self)
        {
            Next = next;
            Prev = prev;
            Self = self;
        }

        public Link Next { get; }

        public Link Prev { get; }

        public Link Self { get; }
    }
}