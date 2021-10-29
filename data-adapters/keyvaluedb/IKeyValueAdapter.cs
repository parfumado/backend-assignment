using System;
using System.Text.Json;
using DataAdapters.KeyValueDb;

namespace DataAdapters.KeyValueDb
{
    public interface IKeyValueAdapter
    {
        public IReader GetReader(DictionaryIndex dbIndex);
        public IWriter GetWriter(DictionaryIndex dbIndex);
    }
}
