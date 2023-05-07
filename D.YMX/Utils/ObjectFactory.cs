using System;
using System.Collections.Generic;

namespace D.YMX.Utils
{
    public enum CountryEnum
    {
        China,
        America,
    }
    public static class ObjectFactory
    {
        private static readonly Dictionary<CountryEnum, IAmazon> Dic = new Dictionary<CountryEnum, IAmazon>();
        private static IAmazon Create(CountryEnum type)
        {
            switch (type)
            {
                case CountryEnum.China:
                    return new AmazonForChina();
                case CountryEnum.America:
                    return new AmazonForAmerica();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        public static IAmazon Get(CountryEnum type)
        {
            if (!Dic.ContainsKey(type))
            {
                Dic.Add(type, Create(type));
            }

            return Dic[type];
        }
    }
}
