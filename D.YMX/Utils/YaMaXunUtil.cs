using System.Collections.Generic;

namespace D.YMX.Utils
{
    public static class YaMaXunUtil
    {
        /// <summary>
        /// 亚马逊国家+域名
        /// </summary>
        public static List<CountryEntity> Countries = new List<CountryEntity>
        {
             new CountryEntity()
            {
                CountryType = CountryEnum.China,
                Name ="中国",  Domain = "https://www.amazon.cn", Mid ="", PlanmId="",Lop="_zh_CN",
                KeywordsUrl ="https://www.amazon.cn/s?k={KEY_WORDS}&page={PAGE}&__mk_zh_CN=亚马逊网站&crid=DWX7P89YDWNF&qid={QID}&sprefix={KEY_WORDS},aps,100&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.cn/dp/{ASIN}?th=1&psc=1",
            },
            new CountryEntity()
            {
                CountryType = CountryEnum.America,
                Name ="美国",  Domain = "https://www.amazon.com", Mid ="ATVPDKIKX0DER", PlanmId="1",Lop="en_US",
                KeywordsUrl ="https://www.amazon.com/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.com/dp/{ASIN}?th=1&psc=1",
            },
            new CountryEntity()
            {
                Name ="德国",  Domain = "https://www.amazon.de", Mid ="A1PA6795UKMFR9", PlanmId="4",Lop="en_GB",
                KeywordsUrl ="https://www.amazon.de/s?k={KEY_WORDS}&page={PAGE}&__mk_de_DE=ÅMÅŽÕÑ&crid=11R1M4U664KVP&qid={QID}&sprefix={KEY_WORDS},aps,370&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.de/dp/{ASIN}?th=1&psc=1",
            },
            new CountryEntity()
            {
                Name ="墨西哥",  Domain = "https://www.amazon.com.mx", Mid ="A1AM78C64UM0Y8", PlanmId="771770",Lop="es_MX",
                KeywordsUrl ="https://www.amazon.com.mx/s?k={KEY_WORDS}&page={PAGE}&__mk_es_MX=ÅMÅŽÕÑ&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.mx/dp/{ASIN}?th=1&psc=1",
            },
            new CountryEntity()
            {
                Name ="澳大利亚",  Domain = "https://www.amazon.com.au", Mid ="A39IBJ37TRP1C6", PlanmId="111172",Lop="en_AU",
                KeywordsUrl ="https://www.amazon.com.au/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.au/dp/{ASIN}?th=1&psc=1",
            },
            new CountryEntity()
            {
                Name ="意大利",  Domain = "https://www.amazon.it", Mid ="APJ6JRA9NG5V4", PlanmId="35691",Lop="it_IT",
                KeywordsUrl ="https://www.amazon.it/s?k={KEY_WORDS}&page={PAGE}&__mk_it_IT=ÅMÅŽÕÑ&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.de/dp/{ASIN}?th=1&psc=1",
            },
            new CountryEntity()
            {
                Name ="波兰",  Domain = "https://www.amazon.pl", Mid ="A1C3SOZRARQ6R3", PlanmId="712115121",Lop="pl_PL",
                KeywordsUrl ="https://www.amazon.pl/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.pl/dp/{ASIN}?th=1&psc=1",
            },
            new CountryEntity()
            {
                Name ="西班牙",  Domain = "https://www.amazon.es", Mid ="A1RKKUPIHCS9HS", PlanmId="44551",Lop="es_ES",
                KeywordsUrl ="https://www.amazon.es/s?k={KEY_WORDS}&page={PAGE}&__mk_es_ES=ÅMÅŽÕÑ&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.es/dp/{ASIN}?th=1&psc=1",
            },
            new CountryEntity()
            {
                Name ="阿联酋",  Domain = "https://www.amazon.ae", Mid ="A2VIGQ35RCS4UG", PlanmId="338801",Lop="en_AE",
                KeywordsUrl ="https://www.amazon.ae/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.ae/dp/{ASIN}?th=1&psc=1",
            },

            new CountryEntity()
            {
                Name ="加拿大",  Domain = "https://www.amazon.ca", Mid ="A2EUQ1WTGCTBG2", PlanmId="7",Lop="en_CA",
                KeywordsUrl ="https://www.amazon.ca/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.ca/dp/{ASIN}?th=1&psc=1",
            },
            new CountryEntity()
            {
                Name ="印度",  Domain = "https://www.amazon.in", Mid ="A21TJRUUN4KGV", PlanmId="44571",Lop="en_IN",
                KeywordsUrl ="https://www.amazon.in/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.in/dp/{ASIN}?th=1&psc=1",

            },new CountryEntity()
            {
                Name ="法国",  Domain = "https://www.amazon.fr", Mid ="A13V1IB3VIYZZH", PlanmId="5",Lop="fr_FR",
                KeywordsUrl ="https://www.amazon.fr/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.fr/dp/{ASIN}?th=1&psc=1",
            },new CountryEntity()
            {
                Name ="瑞典",  Domain = "https://www.amazon.se", Mid ="A2NODRKZP88ZB9", PlanmId="704403121",Lop="sv_SE",
                KeywordsUrl ="https://www.amazon.se/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.se/dp/{ASIN}?th=1&psc=1",
            },new CountryEntity()
            {
                Name ="荷兰",  Domain = "https://www.amazon.nl", Mid ="A1805IZSGTT6HS", PlanmId="328451",Lop="nl_NL",
                KeywordsUrl ="https://www.amazon.nl/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.nl/dp/{ASIN}?th=1&psc=1",
            },new CountryEntity()
            {
                Name ="英国",  Domain = "https://www.amazon.co.uk", Mid ="A1F83G8C2ARO7P", PlanmId="3",Lop="en_GB",
                KeywordsUrl ="https://www.amazon.uk/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.uk/dp/{ASIN}?th=1&psc=1",
            },new CountryEntity()
            {
                Name ="日本",  Domain = "https://www.amazon.co.jp", Mid ="A1VC38T7YXB528", PlanmId="6",Lop="zh_CN",
                KeywordsUrl ="https://www.amazon.jp/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.jp/dp/{ASIN}?th=1&psc=1",
            },
            new CountryEntity()
            {
                Name ="巴西",  Domain = "https://www.amazon.com.br", Mid ="", PlanmId="",Lop="",
                KeywordsUrl ="https://www.amazon.br/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.br/dp/{ASIN}?th=1&psc=1",
            },new CountryEntity()
            {
                Name ="土耳其",  Domain = "https://www.amazon.com.tr", Mid ="", PlanmId="",Lop="",
                KeywordsUrl ="https://www.amazon.tr/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.tr/dp/{ASIN}?th=1&psc=1",
            }
            ,new CountryEntity()
            {
                Name ="比利时",  Domain = "https://www.amazon.com.be", Mid ="", PlanmId="",Lop="",
                KeywordsUrl ="https://www.amazon.be/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.be/dp/{ASIN}?th=1&psc=1",
            },new CountryEntity()
            {
                Name ="沙特",  Domain = "https://www.amazon.sa", Mid ="", PlanmId="",Lop="",
                KeywordsUrl ="https://www.amazon.sa/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.sa/dp/{ASIN}?th=1&psc=1",
            },new CountryEntity()
            {
                Name ="新加坡",  Domain = "https://www.amazon.sg", Mid ="", PlanmId="",Lop="",
                KeywordsUrl ="https://www.amazon.sg/s?k={KEY_WORDS}&page={PAGE}&qid={QID}&ref=sr_pg_{PAGE}",
                ShopUrl ="",
                DetailUrl ="https://www.amazon.sg/dp/{ASIN}?th=1&psc=1",
            }
        };

        /// <summary>
        /// 根据国家获取该国家信息
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public static CountryEntity GetCountryCode(string country)
        {
            return Countries.Find(m => m.Name == country);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="countryEntity"></param>
        /// <returns></returns>
        public static IAmazon Instance(this CountryEntity countryEntity)
        {
            return ObjectFactory.Get(countryEntity.CountryType);
        }
    }
}
