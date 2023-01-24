using Glitonea.Mvvm.Messaging;

namespace BooruApp.Infrastructure.Messages
{
    public class NewSearchMessage : Message
    {
        public string Search { get; }

        public NewSearchMessage(string search)
            => Search = search;
    }
}