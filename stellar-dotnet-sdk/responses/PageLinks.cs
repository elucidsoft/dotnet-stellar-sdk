namespace stellar_dotnet_sdk.responses.page
{
    /// <summary>
    ///     Links connected to page response.
    /// </summary>
    public class PageLinks<T>
    {
        public PageLinks(Link<Page<T>> next, Link<Page<T>> prev, Link<Page<T>> self)
        {
            Next = next;
            Prev = prev;
            Self = self;
        }

        public Link<Page<T>> Next { get; }

        public Link<Page<T>> Prev { get; }

        public Link<Page<T>> Self { get; }
    }
}