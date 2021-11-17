﻿using System;
using System.Collections.Generic;
using HraveMzdy.Legalios.Service.Interfaces;
using HraveMzdy.Procezor.Service.Errors;
using HraveMzdy.Procezor.Service.Interfaces;
using HraveMzdy.Procezor.Service.Types;
using ResultMonad;

namespace HraveMzdy.Procezor.Service
{
    public interface IServiceProcezor
    {
        VersionCode Version { get; }
        IList<ArticleCode> CalcArticles { get; }
        IList<ArticleCode> BuilderOrder { get; }
        IDictionary<ArticleCode, IEnumerable<IArticleDefine>> BuilderPaths { get; }

        IEnumerable<Result<ITermResult, ITermResultError>> GetResults(IPeriod period, IBundleProps ruleset, IEnumerable<ITermTarget> targets);
        bool BuildFactories();
        bool InitWithPeriod(IPeriod period);
        IArticleSpec GetArticleSpec(ArticleCode code, IPeriod period, VersionCode version);
        IConceptSpec GetConceptSpec(ConceptCode code, IPeriod period, VersionCode version);
    }
}
