﻿using System;
using System.Collections.Generic;
using HraveMzdy.Legalios.Service.Interfaces;
using HraveMzdy.Procezor.Registry.Factories;
using HraveMzdy.Procezor.Service.Errors;
using HraveMzdy.Procezor.Service.Interfaces;
using HraveMzdy.Procezor.Service.Types;
using ResultMonad;

namespace HraveMzdy.Procezor.Registry
{
    public interface IResultBuilder<EA, EC>
            where EA : struct, IComparable where EC : struct, IComparable
    {
        VersionCode Version { get; }
        IPeriod PeriodInit { get; } 
        bool InitWithPeriod(VersionCode version, IPeriod period, IArticleSpecFactory articleFactory, IConceptSpecFactory conceptFactory);
        IEnumerable<Result<ITermResult, ITermResultError>> GetResults(IEnumerable<ITermTarget> targets, IArticleDefine finDefs);
        IList<ArticleCode> ArticleOrder { get; }
        IDictionary<ArticleCode, IEnumerable<IArticleDefine>> ArticlePaths { get; }
    }
}
