using Glitonea.Mvvm.Messaging;

namespace BooruApp.Messages
{
    public class NewSearchMessage : Message
    {
        public string Search { get; }

        public NewSearchMessage(string search)
            => Search = search;
    }
}