﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using DnetIndexedDb.Models;

namespace FirstBlazorApp.Models
{
    public class TStore<T> : IndexedDbStore where T : class
    {
        private IndexedDbStoreParameter _key;

        private readonly List<IndexedDbIndex> _indexes = new List<IndexedDbIndex>();

        public TStore()
        {
            BuildStore();
            Name = typeof(T).Name;
            Key = _key;
            Indexes = _indexes;
        }

        private void BuildStore()
        {
            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
               // var classId = info.Name.Substring(info.Name.Length - 2);

                var classAttrs = info.GetCustomAttributes(true);

                var keyAttr = classAttrs.Select(p => p as IndexDbKeyAttribute).FirstOrDefault();

                if (keyAttr != null)
                {
                    _key = new IndexedDbStoreParameter
                    {
                      //  KeyPath = info.Name,
                       // KeyPath = info.Name.ToCamelCase(),
                        KeyPath = info.Name.ToLower(),
                        AutoIncrement = true
                    };
                }

                _indexes.Add(new IndexedDbIndex
                {
                    Name = info.Name.ToLower(),
                    //Name = info.Name.ToCamelCase(),
                    //Name = info.Name,
                    Definition = new IndexedDbIndexParameter { Unique = false }
                });
            }
        }
    }
}
