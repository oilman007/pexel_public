﻿using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.General
{
    public class BinTable<T>
    {
        public BinTable(int n, T default_value)
        {
            Count = n;
            Values = Enumerable.Repeat(default_value, Index(n - 1, n)).ToArray();
            DefaultValue = default_value;   
        }

        public int Count { private set; get; }
        public T[] Values { private set; get; }
        public T DefaultValue { set; get; }

        int Index(int i, int j)
        {
            int min = Math.Min(i, j);
            int max = Math.Max(i, j);
            int result = Count * min - (int)(0.5 * min * min + 1.5 * min + 1) + max;
            return result;
        }

        public bool SetValue(int i, int j, T value)
        {
            if (i == j)
                return false;
            int index = Index(i, j);
            if (index >= Values.Length)
                return false;
            Values[index] = value;
            return true;
        }

        public T GetValue(int i, int j)
        {
            if (i == j)
                return DefaultValue;
            int index = Index(i, j);
            if (index >= Values.Length)
                return DefaultValue;
            return Values[index];
        }



    }
}