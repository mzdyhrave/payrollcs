﻿using System;
using System.Collections.Generic;
using System.Linq;
using HraveMzdy.Legalios.Service.Interfaces;
using HraveMzdy.Procezor.Service.Interfaces;
using HraveMzdy.Procezor.Service.Providers;
using HraveMzdy.Procezor.Service.Types;

namespace HraveMzdy.Procezor.Registry.Factories
{
    public class ArticleProviderConfig : ArticleSpecProvider
    {
        private ArticleSpecConfig articleSpec = null;
        class ArticleSpecConfig : ArticleSpec
        {
            public ArticleSpecConfig(Int32 code, Int32 role, IEnumerable<ArticleCode> sums) : base(code, role)
            {
                Sums = sums.ToList();
            }
        }
        public ArticleProviderConfig(ISpecCode article, ISpecCode concept, IEnumerable<ISpecCode> sums) : base(article.Value)
        {
            articleSpec = new ArticleSpecConfig(article.Value, concept.Value, SpecsToSumsList(sums));
        }
        public ArticleProviderConfig(Int32 article, Int32 concept, IEnumerable<Int32> sums) : base(article)
        {
            articleSpec = new ArticleSpecConfig(article, concept, ConstToSumsList(sums));
        }
        public override IArticleSpec GetSpec(IPeriod period, VersionCode version)
        {
            return articleSpec;
        }
        static IEnumerable<ArticleCode> ConstToSumsList(IEnumerable<Int32> sums) {
            return sums.Select(x => ArticleCode.Get(x));
        }
        static IEnumerable<ArticleCode> SpecsToSumsList(IEnumerable<ISpecCode> sums) {
            return sums.Select(x => ArticleCode.Get(x.Value));
        }
    }
}
