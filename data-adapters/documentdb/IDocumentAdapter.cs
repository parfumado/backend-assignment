using System;
using System.Text.Json;

namespace DataAdapters.DocumentDb
{
    public interface IDocumentAdapter
    {
        public IReader GetReader();
        public IWriter GetWriter();
    }
}
